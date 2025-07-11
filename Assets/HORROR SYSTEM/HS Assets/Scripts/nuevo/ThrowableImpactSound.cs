using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class ThrowableImpactSound : MonoBehaviour
{
    public AudioClip impactSound;
    public float minImpactVelocity = 3f;
    public float detectionRadius = 10f;

    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasPlayed) return;

        if (collision.relativeVelocity.magnitude >= minImpactVelocity)
        {
            hasPlayed = true;

            // Reproduce el sonido de impacto
            if (impactSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(impactSound);
            }

            // Notifica a los enemigos cercanos
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider col in colliders)
            {
                EnemyHearing enemy = col.GetComponent<EnemyHearing>();
                if (enemy != null)
                {
                    enemy.HearSound(transform.position);
                }
            }

            // Destruye el objeto tras reproducir sonido (opcional)
            Destroy(gameObject, impactSound.length);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

