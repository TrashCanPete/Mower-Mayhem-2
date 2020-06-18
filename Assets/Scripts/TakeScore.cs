using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Setup;
using GameAnalyticsSDK.Events;
using UnityEngine.SceneManagement;
public class TakeScore : MonoBehaviour
{
    private int characterNumber;
    public TextMeshProUGUI text;

    private void Start()
    {
        characterNumber = CharacterSelectCam.characterIndex +=1;
    }

    public void SendScore()
    {
#if !UNITY_EDITOR
        AnalyticsController.Controller.SendScore();
        AnalyticsController.Controller.SendName(text.text, Time.time);
        AnalyticsController.Controller.CharacterID(characterNumber);
#endif
        //SceneManager.LoadScene(0);
        Highscores.AddNewHighscore(text.text, Score.Points);
    }
}
