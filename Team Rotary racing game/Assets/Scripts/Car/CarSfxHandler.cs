using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarSfxHandler : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixer audioMixer;

    [Header("Audio sources")]
    public AudioSource tiresAudio;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;
    public float maxEnginePitch = 2f;
    public float SFXvolume;
    public float tmpvolume;

    TopDownCarController topDownCarController;

    void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();
    }

    void Start()
    {
    }

    void Update()
    {
        UpdateEngineSFX();
        UpdateTiresScreechingSFX();

    }

    void UpdateEngineSFX()
    {
        float velocityMagnitude = topDownCarController.GetVelocityMagnitude();

        float desiredEngineVolume = velocityMagnitude * 0.05f;

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, maxEnginePitch);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTiresScreechingSFX()
    {
        if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tiresAudio.volume = Mathf.Lerp(tiresAudio.volume, 1.0f, Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tiresAudio.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else tiresAudio.volume = Mathf.Lerp(tiresAudio.volume, 0, Time.deltaTime * 10);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        float relativeVelocity = collision2D.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        carHitAudioSource.pitch = Random.Range(0.95f, 1.05f);
        carHitAudioSource.volume = volume;

        if (!carHitAudioSource.isPlaying)
            carHitAudioSource.Play();
    }


}
