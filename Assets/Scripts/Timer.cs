using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Setup;
using GameAnalyticsSDK.Events;
using TMPro;
[RequireComponent(typeof(AudioSource))]
public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerUI;
    [SerializeField]
    public static float timeRemaining;
    public bool pause;
    [SerializeField]
    private float startTimerValue;
    public Score score;

    int timesadded = 1;
    int targetPoints = 50;
    public int pointsToGetMoreTime;
    AudioSource audioSource;
    float secondsLeft = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ResetTimer();
        timerUI = GetComponent<TextMeshProUGUI>();
        timerUI.text = startTimerValue.ToString();
        secondsLeft = startTimerValue;
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        timerUI.text = time.ToString(@"m\:ss");
    }

    void Update()
    {
        /* pointsToGetMoreTime = timesadded * targetPoints;
         if(Score.Points > timesadded * targetPoints)
         {
             timesadded++;
             timeRemaining++;
         }*/
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        if (pause == false)
        {
            TimerCountDown();
            timerUI.text = time.ToString(@"m\:ss");
        }
        if (timeRemaining < 11f)
        {
            timerUI.color = Color.red;

            if (time.Seconds < secondsLeft)
            {
                secondsLeft = time.Seconds;
                LastFewSeconds();
            }

        }
        if (timeRemaining <= 0)
        {
            if (!pause)
                EndEvent();
        }
    }
    void LastFewSeconds()
    {
        audioSource.Play();
    }
    void EndEvent()
    {


        GameEvents.Instance.GameEnd.Invoke();
        pause = true;
        timerUI.text = "Time's Up!";
        Invoke("TimesUp", 5);
        AnalyticsController.Controller.TotalGameTime();
    }
    void ResetTimer()
    {
        timeRemaining = startTimerValue;
    }
    void TimerCountDown()
    {
        timeRemaining -= 1 * Time.deltaTime;
    }

    void TimesUp()
    {
#if !UNITY_EDITOR                                                 //currency   //amount    //item type   //item ID
#endif
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetPauseState(bool isPaused)
    {
        pause = isPaused;
    }
}
