using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPos : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smooth;
    Vector3 currentSpeed;
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position,ref currentSpeed ,smooth);
    }
    public void JumpToTarget()
    {
        transform.position = target.position;
    }
}
