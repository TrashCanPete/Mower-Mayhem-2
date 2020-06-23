using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Notifications : MonoBehaviour
{
    private GameObject anim;
    private Animations animScript;
    public static Notifications notifScript;
    TextMeshProUGUI text;
    const float waitTime = 2.5f;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player");
        animScript = anim.GetComponentInChildren<Animations>();
        notifScript = this;
        text = GetComponent<TextMeshProUGUI>();
        text.enabled = false;
    }


    public static void SendNotification(string notif)
    {
        if (notifScript != null)
            notifScript.ShowNotification(notif);
    }
    public void ShowNotification(string notif)
    {
        animScript.Celebrate();
        text.enabled = true;
        text.text = notif;
        StopAllCoroutines();
        StartCoroutine(HideText());
    }
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(waitTime);
        text.text = "";
        text.enabled = false;
    }
}
