using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCam : MonoBehaviour
{
    [SerializeField] Driving driving = null;
    [SerializeField] LerpToPos cameraLerp;
    bool flipped = false;
    private void Update()
    {
        if (driving.Reversing !=flipped)
        {
            Flip();
        }
    }
    void Flip()
    {
        flipped = !flipped;
        transform.Rotate(Vector3.up * 180, Space.Self);
        cameraLerp.JumpToTarget();
    }
}
