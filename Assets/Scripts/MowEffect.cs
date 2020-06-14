using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class MowEffect : MonoBehaviour
{

    public PaintTerrain painter;
    bool show = false;
    ParticleSystem effect;
    private void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (painter.isPainting)
        {
            show = painter.isPainting;
            StopAllCoroutines();
            StartCoroutine(ShowTimeout());
        }
        if (show)
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
    IEnumerator ShowTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        show = false;
    }
}

