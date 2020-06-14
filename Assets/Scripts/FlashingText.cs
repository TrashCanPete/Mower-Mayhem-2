using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FlashingText : MonoBehaviour
{
    TMP_Text flashingText;

    void Start()
    {
        //get the Text component
        flashingText = GetComponent<TMP_Text>();
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
    }

    //function to blink the text 
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            //set the Text's text to blank
            flashingText.text = "";
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.4f);
            //display “INSERT COIN[S]” for the next 0.5 seconds
            flashingText.text = "INSERT COIN[S]";
            yield return new WaitForSeconds(.4f);
        }
    }

}

