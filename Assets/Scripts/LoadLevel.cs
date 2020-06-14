using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public enum LoadTrigger
{
    none,
    start,
    buttonPress,
    anyKey
}
public enum LoadDestination
{
    next,
    same,
    prev,
    number
}

public class LoadLevel : MonoBehaviour
{
    public LoadDestination loadDest;
    [HideInInspector] public int level;
    [HideInInspector] public LoadTrigger trigger;
    [HideInInspector] public string button;
    public void Activate()
    {
        LoadByDest(loadDest);
    }
    public void Activate(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Activate(LoadDestination dest)
    {
        LoadByDest(dest);
    }
    void LoadByDest(LoadDestination dest)
    {
        int levelToGoTo = 0;
        switch (dest)
        {
            case LoadDestination.next:
                levelToGoTo = SceneManager.GetActiveScene().buildIndex + 1;
                break;
            case LoadDestination.prev:
                levelToGoTo = SceneManager.GetActiveScene().buildIndex - 1;
                break;
            case LoadDestination.same:
                levelToGoTo = SceneManager.GetActiveScene().buildIndex;
                break;
            case LoadDestination.number:
                levelToGoTo = level;
                break;
        }
        SceneManager.LoadScene(levelToGoTo);
    }
    private void Start()
    {
        if (trigger == LoadTrigger.start)
        {
            Activate();
        }
    }
    private void Update()
    {
        if (trigger == LoadTrigger.buttonPress)
        {
            if (Input.GetButtonDown(button))
                Activate();
        }
        else if(trigger == LoadTrigger.anyKey)
        {
            if (Input.anyKey)
                Activate();
        }
    }
}
