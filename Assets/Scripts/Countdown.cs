using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(AudioSource))]
public class Countdown : MonoBehaviour
{
    TextMeshProUGUI text;
    public AudioClip[] audioClips = new AudioClip[5];
    AudioSource audioSource;
    int count = 3;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Count());
    }
    IEnumerator Count()
    {
        audioSource.PlayOneShot(audioClips[0]);
        text.text = "";
        yield return new WaitForSeconds(1);
        int clip = 1;
        while (count > 0)
        {
            audioSource.PlayOneShot(audioClips[clip]);
            clip++;
            text.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        audioSource.PlayOneShot(audioClips[audioClips.Length-1]);
        GameEvents.Instance.GameStart.Invoke();
        text.fontSize = text.fontSize - 20;
        text.text = "MOW!";
        yield return new WaitForSeconds(1);
        text.enabled = false;
    }
}
