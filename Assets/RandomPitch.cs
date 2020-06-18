using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class RandomPitch : MonoBehaviour
{
    public float min = 0.8f;
    public float max = 1.2f;
    void Start()
    {
        float rand = Random.Range(min, max);
        GetComponent<AudioSource>().pitch = rand;
    }
}
