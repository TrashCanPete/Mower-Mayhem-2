using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactPoints : MonoBehaviour
{
    public int points;
    const string text = "Destruction";
    const float displayTime = 1;
    bool hit = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player"&&!hit)
        {
            Score.Points += points;
            BonusDisplay.ShowBonus(ScoreTypes.Destruction, "Destruction", points);
            hit = true;
        }
    }
}
