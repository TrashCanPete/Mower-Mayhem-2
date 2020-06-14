using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisioAudio : MonoBehaviour
{
    bool colSoundEnabled = false;
    [SerializeField] float volume = 0.5f;
    [SerializeField] float min = 0.5f;
    [SerializeField] float max = 20f;
    AudioSource audioSource;
    public AudioClip[] clips;
    const float cooldown = 0.1f;
    private void Start()
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.spatialBlend = 1;
        audioSource.volume = volume;
        StartCoroutine(EnableSound(1));
    }
    private void OnValidate()
    {
        if(audioSource!=null)
            audioSource.volume = volume;
    }
    IEnumerator EnableSound(float time)
    {
        yield return new WaitForSeconds(time);
        colSoundEnabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > min&&colSoundEnabled)
        {
            float vol = collision.impulse.magnitude / max;
            PlayRandomSound(vol);
            colSoundEnabled = false;
            StartCoroutine(EnableSound(cooldown));
        }     
    }
    void PlayRandomSound(float vol)
    {
        int random = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[random]);
    }
}
