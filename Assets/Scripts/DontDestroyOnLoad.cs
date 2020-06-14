using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;
    public static DontDestroyOnLoad Instance { get { return instance; } }
    public bool setActiveCanvas = true;
    public GameObject canvas;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
        if(setActiveCanvas == true)
        {
            canvas.SetActive(true);
        }
        else if(setActiveCanvas == false)
        {
            canvas.SetActive(false);
        }
    }
}
