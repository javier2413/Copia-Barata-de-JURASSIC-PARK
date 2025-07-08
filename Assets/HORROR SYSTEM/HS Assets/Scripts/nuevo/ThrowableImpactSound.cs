using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class ThrowableImpactSound : MonoBehaviour
{
    public AudioClip impactSound;
    public float minImpactVelocity = 3f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= minImpactVelocity && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(impactSound);
        }
    }
}

