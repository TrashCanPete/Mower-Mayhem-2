using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevCounter : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    WheelController[] wheels;
    [SerializeField] Driving driving;
    public float[] gearing;
    public float min = 800;
    public float Rpm;
    public float idealRpm;
    public int gear = 1;
    bool changingGear = false;
    const float gearChangeTime = 0.15f;
    public float VerticalInput { get; set; }
    [SerializeField] AudioClip changeUp;
    [SerializeField] AudioClip changeDown;
    [SerializeField] AudioSource source;
    float additionalRpm;
    private void Start()
    {
        NullChecks();
        int count = driving.frontWheels.Length + driving.rearWheels.Length;
        wheels = new WheelController[count];
        int index = 0;
        for (int i = 0; i < driving.frontWheels.Length; i++)
        {
            wheels[index] = driving.frontWheels[i].wheelScript;
            index++;
        }
        for (int i = 0; i < driving.rearWheels.Length; i++)
        {
            wheels[index] = driving.rearWheels[i];
            index++;
        }
    }
    void NullChecks()
    {
        ErrorChecks.NullCheck(driving, nameof(driving));
        ErrorChecks.NullCheck(rb, "Rigidbody");
    }
    private void Update()
    {
        Vector3 vel = rb.velocity;
        vel = transform.InverseTransformVector(vel);
        //Rpm=min+ Mathf.Abs(vel.z * gearing);
        Rpm = (min + GetRPM() * gearing[gear]) + additionalRpm;
        idealRpm = min+ GetIdealRpm() * gearing[gear];
        VerticalInput = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
        if (changingGear)
            VerticalInput = 0;
        CheckGear();
    }
    void CheckGear()
    {
        if (idealRpm > 4000 && !changingGear&& gear < 4)
        {
            StartCoroutine(ChangeUp());
        }
        if (idealRpm < 2000&&!changingGear&& gear > 1)
        {
            StartCoroutine(ChangeDown());
        }
    }
    IEnumerator ChangeUp()
    {
        changingGear = true;
        source.PlayOneShot(changeUp);
        yield return new WaitForSeconds(gearChangeTime);
        if (gear < 4)
        {
            gear++;
        }
        changingGear = false;
    }
    IEnumerator ChangeDown()
    {
        changingGear = true;
        source.PlayOneShot(changeDown);
        yield return new WaitForSeconds(gearChangeTime);
        if (gear > 1)
        {
            gear--;
        }
        changingGear = false;
    }

    float GetRPM()
    {
        float revs = 0;
        for (int i = 0; i < wheels.Length; i++)
        {
            revs += wheels[i].Rpm;
        }
        return revs / 4;
    }
    float GetIdealRpm()
    {
        float revs = 0;
        for (int i = 0; i < wheels.Length; i++)
        {
            revs += wheels[i].IdealRpm;
        }
        return revs / 4;
    }
}
