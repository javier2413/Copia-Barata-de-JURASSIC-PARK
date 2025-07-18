using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour
{

    public float volume = 1.0f; // 1 = normal, 2 = fuerte, etc.
    public float lifeTime = 5f;

    private bool soundPlayed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (soundPlayed) return;

        soundPlayed = true;

        Collider[] enemies = Physics.OverlapSphere(transform.position, 20f); // rango amplio para detectar

        foreach (Collider col in enemies)
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.HearSound(transform.position, volume);
            }
        }

        Destroy(gameObject, lifeTime); // destruir el objeto después
    }
}
