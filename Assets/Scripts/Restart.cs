using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using GameAnalyticsSDK;
using GameAnalyticsSDK.Setup;
using GameAnalyticsSDK.Events;
public class Restart : MonoBehaviour
{
        static GameObject restart;
    public bool replace = false;
    public bool persist = true;
    // Start is called before the first frame update
    void Start()
    {
        if (persist)
            DontDestroyOnLoad(gameObject);
        if (replace)
        {
            Destroy(restart);
            restart = gameObject;
            return;
        }
        if (restart == null)
        {
            restart = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            AnalyticsController.Controller.RestartGame();
            SceneManager.LoadScene(0);
        }
    }
}
