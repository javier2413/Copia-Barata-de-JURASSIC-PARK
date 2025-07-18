using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHearing : MonoBehaviour
{
   
    public NavMeshAgent agent;
    public float waitTime = 3f;

    private Vector3 startPosition;
    private bool investigating = false;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        startPosition = transform.position;

    }

    public void HearSound(Vector3 soundPos)
    {
        if (investigating) return;

        investigating = true;
        agent.SetDestination(soundPos);
        Invoke(nameof(ReturnToStart), waitTime);
    }

    void ReturnToStart()
    {
        agent.SetDestination(startPosition);
        investigating = false;
    }
}
