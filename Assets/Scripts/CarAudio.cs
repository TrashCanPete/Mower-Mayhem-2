using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(RevCounter))]
public class EngineSound
{
    public AudioSource audio;
    public Vector2 position;
    public EngineSound(AudioSource _audio, Vector2 _position)
    {
        audio = _audio;
        position = _position;
    }
}

public class CarAudio : MonoBehaviour
{
    [SerializeField] CarAudioClips clips;
    [SerializeField] AnimationCurve pitchAtRPM;
    [SerializeField] GameObject sourceContainer;

    [Range(0, 1)]
    [SerializeField] float engineVolume;
    [SerializeField] RevCounter revCounter;
    Dictionary<string, EngineSound> engineSounds = new Dictionary<string, EngineSound>();
    public float rpm;
    float throttle;
    Vector2 pos;
    float engineRPM;
    public float max = 7000;
    public bool maxRpm = false;
    const float updateFreq = 60;
    bool cutSound = false;

    EngineSound CreateClip(AudioClip clip, Vector2 position)
    {
        AudioSource source = sourceContainer.AddComponent<AudioSource>();
        source.clip = clip;
        source.playOnAwake = true;
        source.loop = true;
        source.volume = 0;
        source.Play();
        EngineSound engineSound = new EngineSound(source, position);
        return engineSound;
    }

    private void Start()
    {
        NullChecks();
        engineSounds.Add("AccelerationHigh", CreateClip(clips.accelerationHigh, new Vector2(1, 1f)));
        engineSounds.Add("AccelerationLow", CreateClip(clips.accelerationLow, new Vector2(1, 0)));
        engineSounds.Add("DecelerationHigh", CreateClip(clips.decelerationHigh, new Vector2(0, 1f))); ;
        engineSounds.Add("DecelerationLow", CreateClip(clips.decelerationLow, new Vector2(0, 0)));
        StartCoroutine(UpdateAudio());
    }
    void NullChecks()
    {
        ErrorChecks.NullCheck(clips, nameof(clips));
        ErrorChecks.NullCheck(sourceContainer,nameof(sourceContainer));
        ErrorChecks.NullCheck(revCounter,nameof(revCounter));
    }

    IEnumerator FluctuateVolume()
    {
        maxRpm = true;
        while (revCounter.Rpm > max)
        {
            cutSound = !cutSound;
            yield return new WaitForSeconds(0.06f);
        }
        cutSound = false;
        maxRpm = false;
    }

    void SetAudioVolAndPitchByDistance(EngineSound sound)
    {
        float vol = (1 - Vector2.Distance(sound.position, pos)) / 2;
        vol = vol * engineVolume;
        if (cutSound)
            vol = vol * 0.25f;
        sound.audio.volume = vol;
        sound.audio.pitch = pitchAtRPM.Evaluate(revCounter.Rpm / 10000);
    }

    void SetAudioVolAndPitch(EngineSound sound, float volume, float pitch)
    {
        sound.audio.volume = volume;
        sound.audio.pitch = pitch;
    }


    IEnumerator UpdateAudio()
    {
        while (enabled)
        {
            throttle = Mathf.Lerp(throttle, (Mathf.Clamp(revCounter.VerticalInput, 0, 1)), 2 * Time.deltaTime);
            if (engineRPM < revCounter.Rpm)
            {
                engineRPM = Mathf.Lerp(revCounter.Rpm, engineRPM, 0.2f * Time.deltaTime);
            }
            else
            {
                engineRPM = Mathf.Lerp(revCounter.Rpm, engineRPM, 2 * Time.deltaTime);
            }
                
            float tempRPM = Mathf.Clamp((engineRPM - 1300) / 2700, 0, 1);
            // rpm = Mathf.Lerp(rpm, tempRPM, 0.2f);
            rpm = tempRPM * 1f;
            pos = new Vector2(throttle, rpm);
            if (revCounter.Rpm > max)
            {
                revCounter.Rpm = Mathf.Clamp(revCounter.Rpm, 0, max + 1000);
                if(!maxRpm)
                    StartCoroutine(FluctuateVolume());
            }
            for (int i = 0; i < engineSounds.Count; i++)
            {
                SetAudioVolAndPitchByDistance(engineSounds.ElementAt(i).Value);
            }
            /*foreach (KeyValuePair<string, EngineSound> sound in engineSounds)
            {
                SetAudioVolAndPitchByDistance(sound.Value);
            }*/

            /*if (engineRPM > 10000000 & car.VerticalInput > 0 && !maxRpm)
            {
                maxRpm = true;
                float soundTime = engineSounds["AccelerationHigh"].audio.time;
                engineSounds["AccelerationHigh"].audio.clip = clips.accelerateMaxRpm;
                engineSounds["AccelerationHigh"].audio.Play();
                if (engineSounds["AccelerationHigh"].audio.clip.length > soundTime)
                    engineSounds["AccelerationHigh"].audio.time = soundTime;
            }
            else if (engineRPM <1000000 && maxRpm || car.VerticalInput <= 0 && maxRpm)
            {
                maxRpm = false;
                float soundTime = engineSounds["AccelerationHigh"].audio.time;
                engineSounds["AccelerationHigh"].audio.clip = clips.accelerationHigh;
                engineSounds["AccelerationHigh"].audio.Play();
                if (engineSounds["AccelerationHigh"].audio.clip.length > soundTime)
                    engineSounds["AccelerationHigh"].audio.time = soundTime;
            }*/
            yield return new WaitForSeconds(1 / updateFreq);
        }
    }
}
