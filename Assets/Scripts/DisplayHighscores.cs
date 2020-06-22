using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DisplayHighscores : MonoBehaviour
{
    //public TMP_Text[] ScoreRanksColumns;
    public TMP_Text[] ScoreValueColumn;
    public TMP_Text[] ScoreNameColumn;

    Highscores highscoreManager;
    [SerializeField]
    private string columnSpace = "            ";

    void Start()
    {
        /*for (int i = 0; i < ScoreRanksColumns.Length; i ++)
        {
            ScoreRanksColumns[i].text = i + 1 + ". Fetching...";
        }*/
        for (int i = 0; i < ScoreNameColumn.Length; i++)
        {
            ScoreNameColumn[i].text = ". Fetching...";
        }
        for (int i = 0; i < ScoreValueColumn.Length; i++)
        {
            ScoreValueColumn[i].text = ". Fetching...";
        }

        highscoreManager = GetComponent<Highscores>();
        StartCoroutine(RefreshHighscores());
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        /*for (int i = 0; i < ScoreRanksColumns.Length; i++)
        {
            ScoreRanksColumns[i].text = i + 1 + "";
            if(highscoreList.Length > i)
            {
                ScoreRanksColumns[i].text += columnSpace + highscoreList[i].username + columnSpace + highscoreList[i].score;

            }
        }*/
        for (int i = 0; i < ScoreNameColumn.Length; i++)
        {
            ScoreNameColumn[i].text = "";
            if (highscoreList.Length > i)
            {
                string name = highscoreList[i].username;
                if (name.Length > 3)
                    name = name.Remove(3);
                ScoreNameColumn[i].text += name;

            }
        }
        for (int i = 0; i < ScoreValueColumn.Length; i++)
        {
            ScoreValueColumn[i].text = "";
            if (highscoreList.Length > i)
            {
                ScoreValueColumn[i].text += highscoreList[i].score;

            }
        }

    }
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }

}
