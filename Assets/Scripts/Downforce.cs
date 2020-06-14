using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downforce : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float force;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * force);
    }
}
