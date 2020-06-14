using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public MonoBehaviour element;
    public float onTime = 0.1f;
    public float offTime = 0.1f;
    void OnEnable()
    {
        StartCoroutine(Toggle());
    }
    IEnumerator Toggle()
    {
        while (enabled)
        {
            if (element != null)
                element.enabled = true;
            yield return new WaitForSeconds(onTime);
            if (element != null)
                element.enabled = false;
            yield return new WaitForSeconds(offTime);
        }
    }
}
