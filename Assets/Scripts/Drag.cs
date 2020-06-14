using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Driving))]
public class Drag : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    Driving driving;
    [Tooltip("Adjust the amount of force applied based on how much the vehicle is slipping. Y axis represents force multiplier, X axis represents sideways velocity(m/s).")]
    [SerializeField] AnimationCurve driftMultiplier;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        driving = GetComponent<Driving>();
    }
    private void FixedUpdate()
    {
        if (driving.IsGrounded)
        {
            Vector3 vel = rb.velocity;
            vel = transform.InverseTransformVector(vel);
            float force = rb.mass * vel.x;
            float drift = driftMultiplier.Evaluate(driving.DriftFactor);
            force = force * (1 - drift);
            rb.AddForce(-transform.right * force * this.force, ForceMode.Force);
        }
    }
}
