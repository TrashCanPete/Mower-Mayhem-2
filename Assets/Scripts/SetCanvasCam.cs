using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class SetCanvasCam : MonoBehaviour
{
    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        Canvas[] canvas = GameObject.FindObjectsOfType<Canvas>();

        foreach(Canvas c in canvas)
        {
                c.worldCamera = cam;
        }
    }
}
