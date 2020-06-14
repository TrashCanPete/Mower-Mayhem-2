using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameAnalyticsSDK;
using GameAnalyticsSDK.Setup;
using GameAnalyticsSDK.Events;
public class Menu : MonoBehaviour
{
    public DontDestroyOnLoad dontDestroy;
    public GameObject crunchCamera;
    public Canvas renderCanvas;
    public GameObject highScoresGroup;
    public GameObject titleGroup;
    public GameObject controlsGroup;
    public GameObject creditsGroup;


    private void Start()
    {
        //dontDestroy = FindObjectOfType<DontDestroyOnLoad>().GetComponent<DontDestroyOnLoad>();
        crunchCamera = GameObject.FindGameObjectWithTag("CrunchCamera");
        renderCanvas = GetComponent<Canvas>();
        StartCoroutine(SwitchTitleOff());
        
    }

    private void Update()
    {
        if (SceneManager.GetSceneByName("Menu Scene").isLoaded && (Input.anyKey))
        {
            LoadScene(1);
            if (Input.GetButtonDown("Cancel"))
            {
                ExitScene();
            }
        }
        if (SceneManager.GetSceneByName("Character Select Scene").isLoaded)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                LoadMainMenu();
            }
        }
    }
    public void LoadScene(int _Level)
    {
        //dontDestroy.setActiveCanvas = false;
        SceneManager.LoadScene(_Level);
        
    }
    public void SelectCharacter()
    {
        if(CharacterSelectCam.characterIndex == 0)
        {
            //default
            Debug.Log("Default");
        }
        if (CharacterSelectCam.characterIndex == 1)
        {
            //punk
            Debug.Log("Punk");
        }
        if (CharacterSelectCam.characterIndex == 2)
        {
            //other
            Debug.Log("Other");
        }
        Invoke("changeGameScene", 2);
    }
    public void changeGameScene()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadMainMenu()
    {
        //dontDestroy.setActiveCanvas = true;
        SceneManager.LoadScene(0);
    }
    public void ExitScene()
    {
        AnalyticsController.Controller.TotalApplicationTime();
        AnalyticsController.Controller.ExitGame();

        Application.Quit();
    }

    public IEnumerator SwitchTitleOff()
    {
        yield return new WaitForSeconds(6.85f);
        titleGroup.SetActive(false);
        StartCoroutine(SwitchScoresGroupOn());
    }

    public IEnumerator SwitchScoresGroupOn()
    {
        yield return new WaitForSeconds(5f);
        titleGroup.SetActive(false);
        highScoresGroup.SetActive(true);
        StartCoroutine(SwitchOffScoresGroup());
    }
    
    public IEnumerator SwitchOffScoresGroup()
    {
        yield return new WaitForSeconds(9.9f);
        highScoresGroup.SetActive(false);
        StartCoroutine(SwitchControlsOn());
    }

    public IEnumerator SwitchControlsOn()
    {
        yield return new WaitForSeconds(3f);
        controlsGroup.SetActive(true);
        StartCoroutine(SwitchCreditsOn());
    }

    public IEnumerator SwitchCreditsOn()
    {
        yield return new WaitForSeconds(6.5f);
        controlsGroup.SetActive(false);
        creditsGroup.SetActive(true);
        StartCoroutine(SwitchTitleOn());
    }

    public IEnumerator SwitchTitleOn()
    {
        yield return new WaitForSeconds(6.5f);
        titleGroup.SetActive(true);
        creditsGroup.SetActive(false);
        StartCoroutine(SwitchTitleOff());
    }
}
