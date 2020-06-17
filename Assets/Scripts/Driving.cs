using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Steering))]
public class Driving : MonoBehaviour
{
    [System.Serializable]
    public class Wheel
    {
        [HideInInspector]public string name = "Rear Wheel";
        public WheelController wheelScript;

        public Wheel(string _name)
        {
            name = "test";
        }
    }
    [Header("Engine")]
    public float enginePower = 3600;
    public float reversePower = 2000;
    
    [Tooltip("Allows you to adjust the engines power at different speeds. The Y axis represents power and the X axis represents current speed(KPH).")]
    [SerializeField] AnimationCurve powerBySpeed;

    [Range(0, 1)][Tooltip("0 for front wheels drive, 1 for rear wheel drive.")]
    [SerializeField] float powerDistribution;

    [Space(2)]
    [Header("Brakes")]
    public float brakePower;

    public float handbrakePower;

    [Tooltip("0 for front bias, 1 for rear bias.")]
    [Range(0, 1)]
    [SerializeField] float brakeBias;
    
    [Space(2)][Header("Wheels")][Tooltip("Front wheels of the car.")]
    public Wheel[] frontWheels = new Wheel[2] { new Wheel("Left"), new Wheel("Right") };

    [Tooltip("Rear wheels of the car. You can have any number of wheels provided it is at least more than one.")]
    public WheelController[] rearWheels;

    public bool Reversing { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsDrifting { get; private set; }
    public float Speed { get; private set; }
    float zAxis;
    bool handbraking = false;
    Rigidbody rb;
    Vector3 localVelocity;
    const float driftThreshhold = 0.18f;
    public float DriftFactor { get; private set; }


    private void Start()
    {
        NullChecks();
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        zAxis = Input.GetAxis("Vertical");
        handbraking = Input.GetButtonDown("Handbrake");        
    }
    void NullChecks()
    {
        ArrayList tempArray = new ArrayList();
        tempArray.AddRange(frontWheels);
        ErrorChecks.EmptyArrayCheck(tempArray, "FrontWheels");
        
        for(int i =0; i < frontWheels.Length; i++)
        {
            ErrorChecks.NullCheck(frontWheels[i].wheelScript, "FrontWheels.wheelScript");
        }

        tempArray = new ArrayList();
        tempArray.AddRange(rearWheels);
        ErrorChecks.EmptyArrayCheck(tempArray, "RearWheels");
    }
    private void FixedUpdate()
    {
        localVelocity = transform.InverseTransformVector(rb.velocity);
        if (!handbraking)
        {
            if (zAxis > 0)
            {
                Reversing = false;
                ApplyPower(enginePower);
            }
            else if (zAxis < 0)
            {
                if (localVelocity.z < 0.5f)
                {
                    Reversing = true;
                    ApplyPower(reversePower);
                }
                else
                {
                    ApplyBrake(brakePower,false);
                }
            }
        }
        else
        {
            ApplyBrake(handbrakePower, true);
        }
        ApplyGrip();
        Speed = transform.InverseTransformVector(rb.velocity).z;
    }
    void ApplyGrip()
    {
        DriftFactor = 0;
        IsGrounded = false;
        for (int i = 0; i < frontWheels.Length; i++)
        {
            DriftFactor += frontWheels[i].wheelScript.SidewaysSlip;
            frontWheels[i].wheelScript.ApplyGrip(transform, DriftFactor);
            if (frontWheels[i].wheelScript.IsGrounded)
                IsGrounded = true;
        }
        for (int i = 0; i < rearWheels.Length; i++)
        {
            DriftFactor += rearWheels[i].SidewaysSlip;
            rearWheels[i].ApplyGrip(transform, DriftFactor);
            if (rearWheels[i].IsGrounded)
                IsGrounded = true;
        }
        DriftFactor /= (frontWheels.Length + rearWheels.Length);
        IsDrifting = DriftFactor > driftThreshhold ? true : false;
        
    }
    void ApplyPower(float power)
    {
        float targetPower = powerBySpeed.Evaluate(Mathf.Abs(localVelocity.z *3.6f));
        targetPower *= power;
        float frontPower = (targetPower * zAxis) * (1 - powerDistribution);
        float rearPower = (targetPower * zAxis) * (powerDistribution);
        frontPower /= frontWheels.Length;
        rearPower /= rearWheels.Length;
        for (int i = 0; i < frontWheels.Length; i++)
        {
            frontWheels[i].wheelScript.PowerMotor(frontPower);
        }
        for (int i = 0; i < rearWheels.Length; i++)
        {
            rearWheels[i].PowerMotor(rearPower);
        }
    }
    void ApplyBrake(float power, bool slip)
    {
        float frontBrakePower = power * zAxis * (1 - brakeBias);
        float rearBrakePower = power * zAxis * (brakeBias);
        for (int i = 0; i < frontWheels.Length; i++)
        {
            frontWheels[i].wheelScript.Brake(frontBrakePower,slip);
        }
        for (int i = 0; i < rearWheels.Length; i++)
        {
            rearWheels[i].Brake(rearBrakePower,slip);
        }
    }
}