using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static int num;
    void Start()
    {
        GameEvents.Instance.GameEnd.AddListener(TestMethod);
        GameEvents.Instance.GameEnd.Invoke();
    }

    void Update()
    {
        
    }

    void TestMethod()
    {
        Debug.Log("Test Successful");
    }
}
