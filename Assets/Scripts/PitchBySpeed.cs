using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class PitchBySpeed : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSource;
    [SerializeField] Rigidbody rb;
    const float refSpeed = 40;
    float lastSpeed = 0;
    float speedChange = 0;
    [SerializeField] float speedChangeMulti = 0;


    private void FixedUpdate()
    {
        speedChange = rb.velocity.magnitude - lastSpeed;
        lastSpeed = rb.velocity.magnitude;

        float input = rb.velocity.magnitude + (speedChange * speedChangeMulti);
        float pitch = Mathf.Lerp(0.6f, 1.6f, input / refSpeed);
        //float vol = Mathf.Lerp(0.01f, 0.3f, input / 50);
        foreach (AudioSource a in audioSource)
        {
            a.pitch = Mathf.Lerp(a.pitch, pitch, 0.2f);
            // a.volume = Mathf.Lerp(a.volume, vol, 0.2f);
        }


    }
}
