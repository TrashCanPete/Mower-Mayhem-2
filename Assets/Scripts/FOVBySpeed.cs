using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class FOVBySpeed : MonoBehaviour
{
    Camera cam;
    [SerializeField] Rigidbody rb;
    [SerializeField] float min;
    [SerializeField]  float max;
    const float maxSpeed = 30;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        cam.fieldOfView = Mathf.Lerp(min, max, rb.velocity.magnitude / maxSpeed);
    }
}
