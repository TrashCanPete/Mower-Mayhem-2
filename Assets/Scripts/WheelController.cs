using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class WheelController : MonoBehaviour
{
    [Tooltip("Collider used for wheels.")]
    SphereCollider col;
    [HideInInspector] public Rigidbody rb;
    List<ContactPoint> contacts = new List<ContactPoint>();
    Vector3 worldVelocity;
    Vector3 localVelocity;
    public bool IsGrounded { get; private set; }
    public float Rpm { get; private set; }
    public float IdealRpm { get; private set; }

    float brakeFactor = 0;
    public float SidewaysSlip { get; private set; }
    public float ForwardSlip { get; private set; }

    [Header("Grip settings")]
    [Tooltip("This defines how the car transitions between gripping and drifting. the Y axis represents the amount of drift (0 = baseGrip, 1=driftGrip) and the X axis represents sideways velocity(m/s).")]
    public AnimationCurve gripCurve;

#pragma warning disable 0649
    [Tooltip("This slip of the wheel when not sliding. A high number will increase grip however may cause jittering or stiff handling.")]
    [SerializeField] float baseGrip = 4f;
    [Tooltip("This slip of the wheel when sliding. Setting this low will cause you to slide further when drifting.")]
    [SerializeField] float driftGrip = 0.4f;
 
    [Range(0,1)]
    [Tooltip("The higher this number the easier it is to maintain drifts.")]
    [SerializeField] float driftMaintain;

    [Space(2)]
    [Header("Advanced Settings")]
    [Tooltip("This is the maximum amount that the wheel can slide")]
    [SerializeField] float maxSlip;
    [Tooltip("The lower this number the more easily the car will slide")]
    [SerializeField] float slipDiv;

    [Space(2)]
    [Header("Visual Settings")]
    [Tooltip("Object that contains the wheel mesh.")]
    [SerializeField] Transform visualWheel;
    [Tooltip("Particle effect to play when in contact with the ground. Make this a child of the wheel.")]
    [SerializeField] ParticleSystem contactParticles;
    [Tooltip("Particle effect to play when sliding. Make this a child of the wheel.")]
    [SerializeField] ParticleSystem slipParticles;
    #pragma warning restore 0649

    const float slipParticleThreshhold = 0.18f;
    const float airGrip = 0.2f;
    const float maxRpm = 8000; //This should probably go somewhere else but it's here for now;
    const float airRpmIncrease = 3;
    bool onGrass = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    public void PowerMotor(float power)
    {
        if (IsGrounded)
        {
            float powerDiv = power / contacts.Count;
            for (int i = 0; i < contacts.Count; i++)
            {
                ApplyPower(powerDiv, contacts[i].normal);
            }
        }
        else
        {
            Rpm += power * airRpmIncrease * Time.fixedDeltaTime;
            Rpm = Mathf.Clamp(Rpm, 0, maxRpm);
        }
    }
    public void ApplyPower(float power, Vector3 normal)
    {
        Vector3 hitTangent = Vector3.ProjectOnPlane(transform.forward, normal);
        rb.AddForce(hitTangent * power, ForceMode.Force);
    }
    public void ApplyGrip(Transform forward, float drift)
    {
        if (IsGrounded)
        {
            float slipGripping = CalcGrip(forward);
            float slipSliding = CalcGrip(transform);
            float newSlip = Mathf.Lerp(slipGripping, slipSliding, gripCurve.Evaluate(SidewaysSlip));
            float newGrip = Mathf.Lerp(baseGrip, driftGrip, gripCurve.Evaluate(SidewaysSlip));
            newSlip += brakeFactor;
            if (newSlip > SidewaysSlip)
            {
                SidewaysSlip = newSlip;
            }
            else
            {
                SidewaysSlip = Mathf.Lerp(SidewaysSlip, newSlip, 1-driftMaintain);
            }

            Vector3 vel = rb.velocity;
            vel = transform.InverseTransformVector(vel);
            float force = rb.mass * vel.x;
            rb.AddForceAtPosition(-transform.right * force * newGrip, contacts[0].point, ForceMode.Force);

            if (SidewaysSlip > slipParticleThreshhold&&onGrass)
            {
                PlayEffect(slipParticles);
            }
            else
            {
                StopEffect(slipParticles);
            }
        }
    }
    float CalcGrip(Transform relativeTrans)
    {
        Vector3 vel = rb.velocity;
        vel = relativeTrans.InverseTransformVector(vel);
        ForwardSlip = SidewaysSlip * Mathf.Abs(Input.GetAxis("Vertical"));
        float sidewaysVel = vel.x;
        sidewaysVel = Mathf.Clamp(sidewaysVel, -maxSlip, maxSlip);
        float newSlip = Mathf.Abs(sidewaysVel / slipDiv);
        newSlip += brakeFactor;
        return newSlip;
    }
    void CalcRpm()
    {
        float distPerRot = 2 * col.radius * Mathf.PI;
        float timePerRot = (distPerRot / localVelocity.z);
        float rot = (360 / timePerRot) * Time.fixedDeltaTime;
        visualWheel.Rotate(Vector3.right * rot);
        IdealRpm = (1 / timePerRot) * 60;
        Rpm = IdealRpm * (1 + Mathf.Clamp(ForwardSlip - brakeFactor, 0, 0.2f) * 3f);
    }
    private void FixedUpdate()
    {
        worldVelocity = rb.velocity;
        localVelocity = transform.InverseTransformVector(worldVelocity);
        CalcRpm();
        brakeFactor = Mathf.Lerp(brakeFactor, 0, (1-driftMaintain)*2);
        if (!IsGrounded)
            Rpm -= 300 * Time.fixedDeltaTime;
    }
    public void Brake(float power,bool slip)
    {
        if (IsGrounded)
        {
            float friction =1.5f;
            if (slip)
            {
                Vector3 vel = rb.velocity;
                vel = transform.InverseTransformVector(vel);
                float forwardVel = vel.z * Mathf.Abs(vel.z);
                forwardVel = Mathf.Clamp(forwardVel, -maxSlip, maxSlip);
                float newSlip = Mathf.Abs(forwardVel / maxSlip);
                if (newSlip > brakeFactor)
                {
                    brakeFactor = newSlip;
                }
                else
                {
                    brakeFactor = Mathf.Lerp(brakeFactor, newSlip, 1 - driftMaintain);
                }
                friction = 1.5f - gripCurve.Evaluate(brakeFactor);
            }
            
            Vector3 hitTangent = Vector3.ProjectOnPlane(rb.velocity.normalized, contacts[0].normal);
            rb.AddForceAtPosition(hitTangent * power * friction, contacts[0].point, ForceMode.Force);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Grass")
        {
            PlayEffect(contactParticles);
            onGrass = true;
        }
        else
        {
            StopEffect(contactParticles);
            StopEffect(slipParticles);
            onGrass = false;
        }
        IsGrounded = true;
        collision.GetContacts(contacts);
    }
    private void OnCollisionExit(Collision collision)
    {
        StopEffect(slipParticles);
        StopEffect(contactParticles);
        IsGrounded = false;
        SidewaysSlip = SidewaysSlip < airGrip ? airGrip : SidewaysSlip;
    }
    private void StopEffect(ParticleSystem effect)
    {
        if (effect.isPlaying)
            effect.Stop();
    }
    private void PlayEffect(ParticleSystem effect)
    {
        if (!effect.isPlaying)
            effect.Play();
    }
}

