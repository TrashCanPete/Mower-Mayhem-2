using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Speedo : MonoBehaviour
{
    public static Rigidbody rb;
    const float startRotation = 90;
    const float endRotation = 0;
    const float topSpeed = 180;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        float speed = 0;
        if (rb != null)
            speed = rb.velocity.magnitude * 3.6f;
        float angle = Mathf.Lerp(startRotation, endRotation, speed / topSpeed);
        Vector3 euler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(euler.x, euler.y, angle);
    }
}
