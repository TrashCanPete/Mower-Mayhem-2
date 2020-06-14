using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Driving))]
public class Steering : MonoBehaviour
{
    float innerAngle;
    float outerAngle;
#pragma warning disable 0649
    Rigidbody LeftWheel;
    Rigidbody RightWheel;
    [Tooltip("Turning radius of the wheels. Setting this lower makes the car turn sharper. If you want to change it during runtime use SetTurniningAngles()")]
    [SerializeField] float turningRadius;
    float wheelBase = 2.17f; //set these up to calculate automatically pls
    float axleWidth = 1.758f;
    Driving driving;
    [Tooltip("Allows you to adjust how hard the car turns when you are drifting. The Y axis represents a multiplier to turning and the X axis represents the amount of slip.")]
    [SerializeField] AnimationCurve driftingAssist;
    [Tooltip("The force applied to turn the wheels. I reccomend leaving this at the default value unless you have issues.")]
    [SerializeField] float steeringPower = 5000;

    public Animator anim;
    public bool recieveInput = true;

#pragma warning restore 0649
    [HideInInspector] public float xAxis;

    private void Start()
    {
        driving = GetComponent<Driving>();
        if (driving.frontWheels[0] != null)
            LeftWheel = driving.frontWheels[0].wheelScript.rb;
        if (driving.frontWheels.Length > 1)
        {
            if (driving.frontWheels[1] != null)
                RightWheel = driving.frontWheels[1].wheelScript.rb;
        }
        axleWidth = CalcAxleWidth();
        wheelBase = CalcWheelBase();
        SetTurningAngles(turningRadius);
    }
    float CalcAxleWidth()
    {
        if (driving.frontWheels.Length > 1)
        {
            return Vector3.Distance(LeftWheel.position, RightWheel.position);
        }
        else
        {
            return 0;
        }
    }
    float CalcWheelBase()
    {
        Vector3 frontPos;
        if (driving.frontWheels.Length > 1)
        {
            frontPos = (LeftWheel.transform.localPosition + RightWheel.transform.localPosition) / 2;
        }
        else
        {
            frontPos = LeftWheel.position;
        }
        float furthestDist = 0;
        foreach (WheelController w in driving.rearWheels)
        {
            Vector3 pos = w.transform.transform.localPosition;
            pos.x = 0;
            float dist = Vector3.Distance(frontPos, pos);
            if (dist > furthestDist)
            {
                furthestDist = dist;
            }
        }
        return furthestDist;
    }
    public void SetTurningAngles(float radius)
    {
        innerAngle = Mathf.Atan(wheelBase / radius);
        outerAngle = Mathf.Atan(wheelBase / (radius + axleWidth));
        innerAngle *= Mathf.Rad2Deg;
        outerAngle *= Mathf.Rad2Deg;
    }
    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        xAxis = xAxis * (1+driftingAssist.Evaluate(driving.DriftFactor));
        if (!recieveInput)
            xAxis = 0;
    }
    private void FixedUpdate()
    {
        if (xAxis < 0)
        {
            Steer(LeftWheel, innerAngle);
            Steer(RightWheel, outerAngle);
        }
        else if (xAxis > 0)
        {
            Steer(LeftWheel, outerAngle);
            Steer(RightWheel, innerAngle);
        }

        else
        {
            CheckStraight(LeftWheel);
            CheckStraight(RightWheel);
        }
    }
    void CheckStraight(Rigidbody r)
    {
        if (r != null)
        {
            float angle = Quaternion.Angle(r.rotation, transform.rotation);
            if (angle < 0.1f)
            {
                r.MoveRotation(transform.rotation);
            }
            else
            {
                Steer(r, 0);
            }
        }
    }
    void Steer(Rigidbody r, float angle)
    {
        if (r != null)
        {
            Vector3 vector = new Vector3(0, angle * xAxis, 0);
            Vector3 rot = transform.forward;
            Vector3 rotatedVector = Quaternion.AngleAxis(angle * xAxis, transform.up) * rot;
            rotatedVector = rotatedVector.normalized;

            r.AddForceAtPosition(steeringPower * rotatedVector, r.position + r.transform.forward, ForceMode.Force);
            r.AddForceAtPosition(steeringPower * -rotatedVector, r.position - r.transform.forward, ForceMode.Force);

            Debug.DrawRay(r.position - r.transform.forward, steeringPower * -rotatedVector);
            Debug.DrawRay(r.position + r.transform.forward, steeringPower * rotatedVector);
        }
    }
}
