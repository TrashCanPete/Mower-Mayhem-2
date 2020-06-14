using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField]
    private float minForce;
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void explode()
    {
        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
        }
    }
}
