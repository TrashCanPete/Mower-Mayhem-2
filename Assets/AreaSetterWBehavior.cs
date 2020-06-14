using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AreaSetterWBehavior : AreaSetter
{
    public string notif;
    public UnityEvent OnComplete;
    public override void Cleared()
    {
        Score.Points += bonusPoints;
        Timer.timeRemaining += bonusTime;
        BonusDisplay.ShowBonus("Cleared " + areaName, bonusPoints);
        if (OnComplete.GetPersistentEventCount() > 0)
            OnComplete.Invoke();
        Notifications.SendNotification(notif);
    }
}
