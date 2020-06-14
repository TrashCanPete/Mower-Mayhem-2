using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameEvents : MonoBehaviour
{
    private static GameEvents instance = null;

    public static GameEvents Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEvents();
            }
            return instance;
        }
    }

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        if (LevelLoaded.GetPersistentEventCount() > 0)
            LevelLoaded.Invoke();
    }
    public UnityEvent LevelLoaded;
    public UnityEvent StartCountdown;
    public UnityEvent GameStart;
    public UnityEvent GameEnd;
}
