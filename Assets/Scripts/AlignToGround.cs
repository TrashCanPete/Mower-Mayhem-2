using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToGround : MonoBehaviour
{
    [Tooltip("Self righting force applied when grounded")]
    [SerializeField] float groundedForce;
    [Tooltip("Self righting force applied when in the air")]
    [SerializeField] float airForce;
    [Tooltip("Self righting force applied when upside down.")]
    [SerializeField] float upsideDownForce;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        RaycastHit hit;
        float force = groundedForce;
        Vector3 alignment = Vector3.up;
       if(Physics.Raycast(transform.position,-transform.up,out hit, 1))
        {
            alignment = hit.normal;
            Debug.DrawRay(hit.point, hit.normal);
        }
        else
        {
            force =  airForce;
        }
        float angle = Vector3.Angle(transform.up, Vector3.up);
        if (angle > 90)
            force *= upsideDownForce;
        rb.AddTorque(Vector3.Cross(transform.up, alignment)*force,ForceMode.Force);
    }
}
