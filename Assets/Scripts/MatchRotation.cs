using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Canvas))]
public class MatchRotation : MonoBehaviour
{
    Canvas can;
    private void Start()
    {
        can = GetComponent<Canvas>();
    }
    private void Update()
    {
        if (can.worldCamera != null)
        {
            can.transform.rotation = can.transform.rotation;
        }
    }
}
