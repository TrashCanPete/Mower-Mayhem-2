using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class LongGrassEffect : MonoBehaviour
{

    public PaintTerrain painter;
    public static bool longGrassCut = false;
    public bool viewLongGrass;
    bool show = false;
    ParticleSystem effect;
    private void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        viewLongGrass = longGrassCut;
        if (longGrassCut == true)
        {
            show = true;
            Debug.Log("longgrass == true");
            Invoke("stopPlaying", 0.5f);
            StopAllCoroutines();
            StartCoroutine(ShowTimeout());
        }
        if (show == true)
        {
            if (!effect.isPlaying)
                effect.Play();
        }
        else
        {
            if (effect.isPlaying)
                effect.Stop();
        }
    }
    void stopPlaying()
    {
        Debug.Log("long grass cut!");
        show = false;
        longGrassCut = false;
    }

    IEnumerator ShowTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("long grass cut!");
        show = false;
        longGrassCut = false;
    }
}

