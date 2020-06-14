using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameTransfer : MonoBehaviour
{
    public string theName;
    public GameObject inputField;
    public GameObject textDisplay;

    public DontDestroyOnLoad dontDestroy;

    private void Start()
    {
        if (FindObjectOfType<DontDestroyOnLoad>()!=null)
            dontDestroy = FindObjectOfType<DontDestroyOnLoad>().GetComponent<DontDestroyOnLoad>();
    }

    public void storeName()
    {
        theName = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "" + theName;
        #if !UNITY_EDITOR
        Highscores.AddNewHighscore(theName, Score.Points);
        #endif
        if(dontDestroy!=null)
            dontDestroy.setActiveCanvas = true;
        SceneManager.LoadScene(0);
    }
}
