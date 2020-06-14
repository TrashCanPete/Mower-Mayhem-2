using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BonusDisplay : MonoBehaviour
{
    public GameObject display;
    public static BonusDisplay bonusScript;
    const float waitTime = 3;
    const float offset = 0.08f;
    const int max = 4;
    List<GameObject> bonuses = new List<GameObject>();
    private void Start()
    {
        bonusScript = this;
    }
    public static void ShowBonus(string text, float bonus, float time = waitTime)
    {
        if (bonusScript != null)
            bonusScript.Show(text, bonus,time);
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
        StartCoroutine(RemoveTime(go,time));
    }
    IEnumerator RemoveTime(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        if (go != null)
        {
            bonuses.Remove(go);
            Destroy(go);
        }
    }
}
