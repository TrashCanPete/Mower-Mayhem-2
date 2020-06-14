using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;
using TMPro;
public class Score : MonoBehaviour
{
    static bool scoring = false;
    static int points;
    float vel = 0;
    Vector3 startPos;
    public Color startCol;
    public Color scoringCol;
    Material textMat = null;
    public static int Points
    {
        get { return points; }
        set 
        {
            if (value > points)
                scoring = true;
            points = value;
        }
    }
    public TextMeshProUGUI scoreText;
    //float desiredScale = 1;
    const float maxSize = 2f;
    const float minSize = 1f;
    float scoringVal = 0;
    void Start()
    {
        startPos = transform.localPosition;
        scoreText = GetComponent<TextMeshProUGUI>();
        Points = 0;
        textMat = new Material(scoreText.fontSharedMaterial);
        scoreText.fontMaterial = textMat;
    }
    void Update()
    {
        if (scoring)
        {
            scoringVal = 1;
            vel = 0;
            scoring = false;
            SetPos();
        }
        else
        {
            ResetPos();
        }
        scoreText.text = ("" + Points);
        SetScoringProperties();
        
    }
    void ResetPos()
    {
        transform.localPosition = startPos;
    }
    void SetPos()
    {
        Vector3 pos = startPos + (Vector3)Random.insideUnitCircle*2;
        transform.localPosition = pos;
    }

    void SetScoringProperties()
    {
        scoringVal = Mathf.SmoothDamp(scoringVal, 0, ref vel, 0.5f);

        float scale = Mathf.Lerp(minSize, maxSize, scoringVal);
        transform.localScale = new Vector3(scale, scale, 1);

        Color col = Color.Lerp(startCol, scoringCol, scoringVal);
        scoreText.color = col;

        float glow = Mathf.Lerp(0, 0.5f, scoringVal);
        textMat.SetFloat("_GlowOuter", glow);
    }
}
