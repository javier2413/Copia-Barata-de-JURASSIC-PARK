using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThrowableManager : MonoBehaviour
{
    public GameObject throwablePrefab;
    public Transform throwOrigin;
    public float throwForce = 15f;
    public float throwDelay = 0.3f;
    private float throwCooldown = 1f;


    public Image throwIcon; // Ícono en la UI

    private float lastThrowTime;
    private bool canThrow = true;


    private Color originalColor;
    private Color cooldownColor = Color.gray;


    void Start()
    {
        if (throwIcon != null)
        {
            originalColor = throwIcon.color;
        }
    }

    void Update()
    {
        float elapsed = Time.time - lastThrowTime;

        if (Input.GetKeyDown(KeyCode.G) && canThrow && elapsed >= throwCooldown)
        {
            StartCoroutine(DelayedThrow());
            lastThrowTime = Time.time;

            // ícono en gris
            if (throwIcon != null)
                throwIcon.color = cooldownColor;
        }

        // Recupera el color cuando el cooldown termina
        if (!canThrow && Time.time >= lastThrowTime + throwCooldown)
        {
            canThrow = true;
            if (throwIcon != null)
                throwIcon.color = originalColor;
        }
    }

    IEnumerator DelayedThrow()
    {
        canThrow = false;
        yield return new WaitForSeconds(throwDelay);
        ThrowItem();
    }


    void ThrowItem()
    {
        GameObject obj = Instantiate(throwablePrefab, throwOrigin.position, Quaternion.identity);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
            
    }
}



