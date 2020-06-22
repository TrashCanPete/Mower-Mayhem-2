using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Setup;
using GameAnalyticsSDK.Events;
public class AnalyticsController : MonoBehaviour
{
    public static AnalyticsController Controller;
    public bool replace = false;
    public bool persist = true;
    void Start()
    {
        if (persist)
            DontDestroyOnLoad(gameObject);
        if (replace)
        {
            Destroy(Controller);
            Controller = this;
            return;
        }
        if (Controller == null)
        {
            Controller = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SendScore()
    {
#if !UNITY_EDITOR

        //tracking player score at end of game
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Points", Score.Points, "Mowed_Grass", "score_ID");
#endif
    }
    public void SendName(string playerName, float totalTime)
    {
#if !UNITY_EDITOR
        //tracking player name at end of game
        GameAnalytics.NewDesignEvent("playerName:Score", Score.Points);
        GameAnalytics.NewDesignEvent("playerName:TotalAreasCleared", AreaSetter.areasCleared);
        GameAnalytics.NewDesignEvent("playerName:TotalTime", totalTime);
#endif
    }
    public void CharacterID(int characterNumber)
    {
#if !UNITY_EDITOR
        //tracking what character was used
        //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Character", characterNumber, "Player_Name", "Player_ID");

        float slashley = characterNumber == 1 ? 100:0;
        float chip = characterNumber == 0 ? 100 : 0;
        GameAnalytics.NewDesignEvent("Slashley pick", slashley);
        GameAnalytics.NewDesignEvent("Chip pick", chip);
#endif
    }
    public void TotalApplicationTime()
    {
#if !UNITY_EDITOR
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "TimeOpened", Time.time, "Game_Time", "ApplicationTime_Total");
#endif
    }
    public void TotalGameTime()
    {
#if !UNITY_EDITOR
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "GameTime", Time.timeSinceLevelLoad, "Game_Time", "LevelTime_Total");
#endif
    }
    public void RestartGame(string playerName)
    {
#if !UNITY_EDITOR
        GameAnalytics.NewDesignEvent("RestartGame:playerName ");
#endif
    }
    public void ExitGame(float totalTime)
    {
//#if !UNITY_EDITOR
        GameAnalytics.NewDesignEvent("ExitGame:", totalTime);
        GameAnalytics.NewDesignEvent("ExitGame:TotalAreasCleared", AreaSetter.areasCleared);
//#endif
    }

}
