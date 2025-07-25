using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dilofo : MonoBehaviour
{
    public Transform player;
    public float rangoPersecucion = 10f;
    public float rangoCeguera = 5f;
    public float velocidad = 3.5f;
    public float tiempoCeguera = 2f;
    public float cooldownCeguera = 5f; // Tiempo entre cegueras

    public GameObject blindPanel;

    private NavMeshAgent agent;
    private bool cegueraActiva = false;
    private bool enCooldown = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidad;

        if (blindPanel != null)
            blindPanel.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        // Movimiento del enemigo
        if (distancia <= rangoPersecucion)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }

        // Activar ceguera si está en rango y no está en cooldown
        if (distancia <= rangoCeguera && !cegueraActiva && !enCooldown)
        {
            StartCoroutine(ActivarCeguera());
        }
    }

    IEnumerator ActivarCeguera()
    {
        cegueraActiva = true;
        enCooldown = true;

        if (blindPanel != null)
            blindPanel.SetActive(true);

        yield return new WaitForSeconds(tiempoCeguera);

        if (blindPanel != null)
            blindPanel.SetActive(false);

        cegueraActiva = false;

        // Espera cooldown antes de permitir otra ceguera
        yield return new WaitForSeconds(cooldownCeguera);
        enCooldown = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoPersecucion);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, rangoCeguera);
    }

}
