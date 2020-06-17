using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCam : MonoBehaviour
{
    [SerializeField] Driving driving = null;
    [SerializeField] LerpToPos cameraLerp;
    bool flipped = false;
    const float speedThreshhold = 6;
    private void Update()
    {
        if (driving.Reversing&&!flipped)
        {
            if (driving.Speed < -speedThreshhold)
                Flip();
        }
        if (!driving.Reversing&&flipped)
        {
            if (driving.Speed > -speedThreshhold)
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
