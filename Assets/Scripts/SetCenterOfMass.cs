using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SetCenterOfMass : MonoBehaviour
{
    Rigidbody rb;
    [Tooltip("Center of mass will be set to the position of this object.")]
    [SerializeField] Transform target;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = target.position-rb.position;
    }
}
