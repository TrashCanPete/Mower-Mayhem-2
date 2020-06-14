using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysDrag : MonoBehaviour
{
    Rigidbody rb;
    public float brakeFactor = 0;
    public float driftFactor = 0;
    public AnimationCurve gripCurve;
    [SerializeField] float multi;
    [SerializeField] float maxSlip;
    [SerializeField] float slipDiv;
    [SerializeField] float slipReduce;
    public float brakePower = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void ApplyGrip(Vector3 point, Transform relativeTrans,float sharedDriftFactor)
    {

        float slipGripping = CalcGrip(relativeTrans);
        float slipSliding = CalcGrip(transform);
        float newSlip = Mathf.Lerp(slipGripping, slipSliding, gripCurve.Evaluate(driftFactor));
        float newGrip = Mathf.Lerp(5, 0.5f, gripCurve.Evaluate(driftFactor));

        if (newSlip > driftFactor)
        {
            driftFactor = newSlip;
        }
        else
        {
            driftFactor = Mathf.Lerp(driftFactor, newSlip, slipReduce);
        }

        Vector3 vel = rb.velocity;
        vel = transform.InverseTransformVector(vel);
        float force = rb.mass * vel.x;

        //vel.x = vel.x * newGrip;
        rb.AddForceAtPosition(-transform.right*force * newGrip, point, ForceMode.Force);
        //rb.velocity =transform.TransformVector(vel);
    }

     float CalcGrip(Transform relativeTrans)
    {
        Vector3 vel = rb.velocity;
        vel = relativeTrans.InverseTransformVector(vel);
        float sidewaysVel = vel.x;
        sidewaysVel = Mathf.Clamp(sidewaysVel, -maxSlip, maxSlip);
        float newSlip = Mathf.Abs(sidewaysVel / slipDiv);
        newSlip += brakeFactor;
        return newSlip;
    }
    private void FixedUpdate()
    {
        brakeFactor = Mathf.Lerp(brakeFactor, 0, slipReduce * 2);
    }
    public void Brake(Vector3 contact, Vector3 normal, float power)
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
            brakeFactor = Mathf.Lerp(brakeFactor, newSlip, slipReduce);
        }
        float friction = gripCurve.Evaluate(brakeFactor);
        Debug.Log(-rb.velocity.normalized * power * friction);
        if (rb.velocity.magnitude > 0.5f)
        {
            Vector3 hitTangent = Vector3.ProjectOnPlane(rb.velocity.normalized, normal);
            rb.AddForceAtPosition(hitTangent * power * friction, contact, ForceMode.Force);
        }
        else
        {
            rb.velocity = rb.velocity * 0.9f;
        }
    }
}
