using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    const string privateCode = "zGZbIfsJfEaRT1df-mGrbARJdRVhGMcUmIfNYuFoo2jw";
    const string publicCode = "5ecb22ad377dce0a1433afde";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;

    private static Highscores instance;
    public static Highscores Instance { get { return instance; } }

    DisplayHighscores highscoresDisplay;



    private void Awake()
    {

        /*if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }*/
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighscores>();
    }
    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }
    IEnumerator UploadNewHighscore(string username, int score)
    {
        UnityWebRequest www = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {
            print("upload successful");
            DownloadHighscores();
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        UnityWebRequest www = new UnityWebRequest(webURL + publicCode + "/pipe/");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();


        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.downloadHandler.text);
            highscoresDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];//getting the name data in slot 0
            int score = int.Parse(entryInfo[1]);//getting score as a string and converting it into a int
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}


