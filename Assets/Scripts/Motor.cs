using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour
{
    [SerializeField] LayerMask drivingLayers;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

}
