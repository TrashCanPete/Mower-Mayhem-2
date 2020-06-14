using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text += " "+ Score.Points;
    }
}
