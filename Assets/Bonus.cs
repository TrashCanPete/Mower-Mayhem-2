using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Bonus : MonoBehaviour
{
    public float amount = 0;
    public ScoreTypes type;
    public string title;
    const float time = 2;
    TextMeshProUGUI text;
    public BonusDisplay bd;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(HideTime());
        text.text = title + " +" + amount;
    }
    public void AddPoints(float amt)
    {
        amount += amt;
        StopAllCoroutines();
        StartCoroutine(HideTime());
        if(text==null)
            text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = title + " +" + amount;
    }
    
    IEnumerator HideTime()
    {
        yield return new WaitForSeconds(time);
        bd.RemoveElement(this);
        Destroy(gameObject);
    }
}
