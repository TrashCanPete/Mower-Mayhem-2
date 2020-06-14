using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AreaDisplay : MonoBehaviour
{
    static string area;
    static TextMeshProUGUI text;
    public static List<AreaSetter> setters = new List<AreaSetter>();

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public static string Area
    {
        get { return area; }
        set
        {
            area = value;
            UpdateText();
        }
    }

    public static void AddArea(AreaSetter s)
    {
        setters.Add(s);
        UpdateText();
    }
    public static void RemoveArea(AreaSetter s)
    {
        setters.Remove(s);
        UpdateText();
    }
    public static void UpdateText()
    {
        if (setters.Count == 0)
        {
            if (text != null)
                text.text = "";
        }
        else
        {
            AreaSetter setter = setters[setters.Count - 1];
            string displayText = setter.areaName;
            int remainingGrass = setter.grassCount - setter.grass.Count;
            displayText += " " + remainingGrass + "/" + setter.grassCount;
            text.text = displayText;
        }
    }
}
