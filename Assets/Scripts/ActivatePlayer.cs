using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayer : MonoBehaviour
{
    public static Rigidbody playerRB;
    public void Deactivate()
    {
        playerRB.isKinematic = true;
    }
    public void Activate()
    {
        playerRB.isKinematic = false;
    }
    public void SpeedBoost(float force)
    {
        StartCoroutine(ApplyForce(force, 0.3f));
    }
    IEnumerator ApplyForce(float force,float time)
    {
        while (time > 0)
        {
            playerRB.AddForce(playerRB.transform.forward * force,ForceMode.Force);
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
        }
    }
}
