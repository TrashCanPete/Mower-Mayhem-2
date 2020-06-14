using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public void Activate()
    {
        ScoreScreenEvents events = Object.FindObjectOfType<ScoreScreenEvents>();
        events.ShowScores.Invoke();
    }
}
