using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    static GameObject music;
    public bool replace = false;
    public bool persist = true;
    private void Start()
    {
        if (persist)
            DontDestroyOnLoad(gameObject);
        if (replace)
        {
            Destroy(music);
            music = gameObject;
            return;
        }
        if (music == null)
        {
            music = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
