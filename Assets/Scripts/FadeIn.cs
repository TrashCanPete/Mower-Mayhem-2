using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class FadeIn : MonoBehaviour
{
    public float time = 0.5f;
    Image img;
    float vel;
    float alpha = 1;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        alpha = Mathf.SmoothDamp(alpha, 0, ref vel, time);
        Color col = img.color;
        col.a = alpha;
        img.color = col;
    }
}
