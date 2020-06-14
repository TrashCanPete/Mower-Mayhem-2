using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Driving))]
[RequireComponent(typeof(Steering))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Driving driving;
    Steering steering;
    Rigidbody rb;
    private void Awake()
    {
        driving = GetComponent<Driving>();
        steering = GetComponent<Steering>();
        rb = GetComponent<Rigidbody>();
        ActivatePlayer.playerRB = rb;
        Speedo.rb = rb;
    }
    private void Update()
    {
        if (Timer.timeRemaining < 0)
            Disable();
    }
    public void Disable()
    {
        driving.enabled = false;
        steering.recieveInput = false;
        rb.drag = 4;
    }
}
