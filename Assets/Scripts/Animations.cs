using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Driving))]
[RequireComponent(typeof(Steering))]
public class Animations : MonoBehaviour
{
    public Animator anim;
    private Driving driving;
    private Steering steering;
    const float cooldown = 0.1f;
    bool canCrash = true;

    // Start is called before the first frame update
    void Start()
    {
        driving = GetComponent<Driving>();
        steering = GetComponent<Steering>();
    }

    // Update is called once per frame
    void Update()
    {
        if (steering.xAxis > 0)
        {
            anim.SetBool("Right Turn", true);
            anim.SetBool("Left Turn", false);
        }
        else if (steering.xAxis < 0)
        {
            anim.SetBool("Left Turn", true);
            anim.SetBool("Right Turn", false);
        }
        else
        {
            anim.SetBool("Left Turn", false);
            anim.SetBool("Right Turn", false);
        }

        if (steering.xAxis > 0 && Input.GetButtonDown("Handbrake"))
        {
            anim.SetBool("Drift Right", true);
            anim.SetBool("Drift Left", false);
        }

        if (steering.xAxis < 0 && Input.GetButtonDown("Handbrake"))
        {
            anim.SetBool("Drift Left", true);
            anim.SetBool("Drift Right", false);
        }

        else if (Input.GetButtonUp("Handbrake"))
        {
            anim.SetBool("Drift Left", false);
            anim.SetBool("Drift Right", false);
        }

        if (driving.Reversing == false)
        {
            anim.SetBool("Reversing", false);
            anim.SetBool("Reverse Right", false);
            anim.SetBool("Reverse Left", false);
        }

        if (driving.Reversing == true && steering.xAxis == 0)
        {
            anim.SetBool("Reversing", true);
            anim.SetBool("Reverse Right", false);
            anim.SetBool("Reverse Left", false);
        }

        
        if (driving.Reversing == true && steering.xAxis > 0)
        {
            anim.SetBool("Reverse Right", true);
            anim.SetBool("Reverse Left", false);
        }

        if (driving.Reversing == true && steering.xAxis < 0)
        {
            anim.SetBool("Reverse Right", false);
            anim.SetBool("Reverse Left", true);
        }


        if (Timer.timeRemaining <= 0)
        {
            anim.SetTrigger("Finish Game");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle" && canCrash&&Mathf.Abs(driving.Speed)>6)
        {
            if (driving.Reversing)
            {
                anim.SetTrigger("Reverse Crashing");
            }
            else
            {
                anim.SetTrigger("Crashing");
            }
            canCrash = false;
            StartCoroutine(CrashCooldown());
        }
    }
    IEnumerator CrashCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canCrash = true;
    }

    public void Celebrate()
    {
        anim.SetTrigger("Celebrating");
    }
}
