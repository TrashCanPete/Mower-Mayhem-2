using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AreaSetter : MonoBehaviour
{
    public static int areasCleared;
    public string areaName;
    public int bonusPoints;
    public int bonusTime;
    [HideInInspector] public List<GameObject> grass = new List<GameObject>();
    [HideInInspector] public int grassCount = 0;
    [HideInInspector] public bool touchingPlayer = false;
    bool contained = false;
    private void Start()
    {
        grass.Clear();
        PickUp[] pickups = GetComponentsInChildren<PickUp>();
        for (int i = 0; i < pickups.Length; i++)
        {
            grass.Add(pickups[i].gameObject);
        }
        grassCount = grass.Count;
    }

    public void RemoveObj(GameObject g)
    {
        grass.Remove(g);
        if (grass.Count == 0)
        {
            Cleared();
        }

        AreaDisplay.UpdateText();
    }
    public virtual void Cleared()
    {
        Score.Points += bonusPoints;
        Timer.timeRemaining += bonusTime;
        areasCleared++;
        Notifications.SendNotification("Cleared " + areaName + " +" + bonusTime + " seconds!");
        FindObjectOfType<Animations>().Celebrate();
        BonusDisplay.ShowBonus(ScoreTypes.ClearedArea, "Cleared " + areaName, bonusPoints);
        Destroy(gameObject);
    }
    public void AddObj(GameObject g)
    {
        grass.Add(g);
        AreaDisplay.UpdateText();
    }
    private void FixedUpdate()
    {
        touchingPlayer = false;
    }
    private void LateUpdate()
    {
        if (touchingPlayer && !contained)
        {
            AddSelf();
        }
        if (!touchingPlayer && contained)
        {
            RemoveSelf();
        }
    }
    void RemoveSelf()
    {
        contained = false;
        AreaDisplay.RemoveArea(this);
    }
    void AddSelf()
    {
        contained = true;
        AreaDisplay.AddArea(this);
    }
}

