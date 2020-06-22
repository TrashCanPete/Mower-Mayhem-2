using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum ScoreTypes { Destruction, CutWeeds, ClearedArea }

public class BonusDisplay : MonoBehaviour
{
    public GameObject display;
    public static BonusDisplay bonusScript;
    const float waitTime = 3;
    const float offset = 0.08f;
    const int max = 4;
    List<Bonus> bonuses = new List<Bonus>();
    public void AddBonus(ScoreTypes type, string name, float amount)
    {
        if (bonuses.Count > 0)
        {
            if (bonuses[bonuses.Count - 1].type == type)
            {
                bonuses[bonuses.Count - 1].AddPoints(amount);
                return;
            }
        }
        GameObject go = Instantiate(display, transform);
        TextMeshProUGUI textElement = go.GetComponentInChildren<TextMeshProUGUI>();
        Bonus b = go.GetComponent<Bonus>();
        b.title = name;
        b.type = type;
        b.amount = amount;
        b.bd = this;
        bonuses.Add(b);
        foreach (Bonus bonus in bonuses)
        {
            if (bonus != null)
                bonus.transform.Translate(Vector3.up * offset);
        }
    }
    public static void ShowBonus(ScoreTypes type, string name, float amount)
    {
        if (bonusScript != null)
            bonusScript.AddBonus(type, name, amount);
    }
    private void Start()
    {
        bonusScript = this;
    }
    public void RemoveElement(Bonus b)
    {
        bonuses.Remove(b);
    }
    /*public static void ShowBonus(string text, float bonus, float time = waitTime)
    {
        if (bonusScript != null)
            bonusScript.Show(text, bonus, time);
    }

    public void Show(string text, float bonus, float time)
    {
        GameObject go = Instantiate(display, transform);
        TextMeshProUGUI textElement = go.GetComponentInChildren<TextMeshProUGUI>();
        textElement.text = text + " +" + bonus;

        if (bonuses.Count > max)
        {
            GameObject temp = bonuses[0];
            bonuses.RemoveAt(0);
            Destroy(bonuses[0]);
        }
        foreach (GameObject g in bonuses)
        {
            if (g != null)
                g.transform.Translate(Vector3.up * offset);
        }
        bonuses.Add(go);
        StartCoroutine(RemoveTime(go, time));
    }
   */
}
