using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseObj;
    [SerializeField]
    public bool pause;
    public static bool canPauseGame = false;
    private void Awake()
    {
    }
    private void Update()
    {
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = false;
                Time.timeScale = 1;
                pauseObj.SetActive(false);
                SceneManager.LoadScene(0);
            }
        }

        pause = isPaused;
        if (Input.GetButtonDown("Cancel") && SceneManager.GetSceneByName("Main Level").isLoaded)
        {
            isPaused = !isPaused;
            if (isPaused == true)
            {
                Time.timeScale = 0;
                pauseObj.SetActive(true);

            }
            else if (isPaused == false)
            {
                Time.timeScale = 1;
                pauseObj.SetActive(false);
            }
        }
        /*if (canPauseGame == false)
        {
            return;
        }
        else
        {

        }
        */
        /*if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }*/

    }
}
