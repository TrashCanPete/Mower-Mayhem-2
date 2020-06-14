using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyThisObject", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void destroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
