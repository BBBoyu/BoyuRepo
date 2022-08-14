using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    public CameraShake cameraShake;


    void OnCollisionEnter2D(Collision2D collision2D)
    {
        float relativeVelocity = collision2D.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;
        float x = relativeVelocity * 0.005f;
        float y = relativeVelocity * 0.01f;
        StartCoroutine(cameraShake.Shake(x, y));
        
    }

}
