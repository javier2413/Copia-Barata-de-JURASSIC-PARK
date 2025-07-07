using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Visibility Settings")]
    public int visibleRange = 5;

    [Header("Combat Settings")]
    public GameObject handDamage;
    public float attackDistance = 1f;
    public float chaseDistance = 2f;
    public float rotationSpeed = 5f;

    public AudioSource enemySound;

    private Transform target;
    private Transform player;
    private NavMeshAgent navAgent;
    private EnemyAnimations enemyAnimations;
    private EnemyHealth enemyHealth;
    private bool isAttacking = false;

    private void Start()
    {
        InitializeEnemy();
    }

    private void Update()
    {
        if (enemyHealth.IsEnemyDead()) return;

        HandleTargeting();
        HandleMovement();
    }

    private void InitializeEnemy()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyAnimations = GetComponent<EnemyAnimations>();
        enemyHealth = GetComponent<EnemyHealth>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        handDamage.GetComponent<Collider>().enabled = false;
    }

    private void HandleMovement()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth.IsPlayerDead())
        {
            StopEnemy();
            return;
        }


        if (target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (isAttacking)
            {
                RotateTowardsPlayer();
                return;
            }

            if (distanceToPlayer > chaseDistance)
            {
                enemyAnimations.StopAttacking();
                enemyAnimations.Walk();
                navAgent.isStopped = false;
                navAgent.SetDestination(target.position);
            }
            else if (distanceToPlayer <= attackDistance)
            {
                navAgent.isStopped = true;
                enemyAnimations.StopWalking();
                enemyAnimations.Attack();
                isAttacking = true;
                Invoke("FinishAttack", 2.2f);
            }
            else
            {
                enemyAnimations.StopAttacking();
                enemyAnimations.Walk();
                navAgent.isStopped = false;
                navAgent.SetDestination(target.position);
            }
        }
        else
        {
            StopEnemy();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void HandleTargeting()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= visibleRange)
        {
            target = player;
        }
        else
        {
            target = null;
            StopEnemy();
        }
    }

    public void StopEnemy()
    {
        enemyAnimations.StopWalking();
        navAgent.isStopped = true;
        navAgent.ResetPath();
    }

    private void FinishAttack()
    {
        isAttacking = false;
        enemyAnimations.StopAttacking();
    }

    public void EnableAttackCollider()
    {
        handDamage.GetComponent<Collider>().enabled = true;
    }

    public void DisableAttackCollider()
    {
        handDamage.GetComponent<Collider>().enabled = false;
    }

    public void DisableEnemySound()
    {
        enemySound.Stop();
    }

    [Header("Hearing Settings")]
    public float hearingRange = 10f;

    // Llamado por cualquier sonido en el mundo (disparos, pasos fuertes, etc.)
    public void HearSound(Vector3 soundPosition, float volume)
    {
        if (enemyHealth.IsEnemyDead()) return;

        float distanceToSound = Vector3.Distance(transform.position, soundPosition);
        if (distanceToSound <= hearingRange * volume)
        {
            target = null; // No atacar de inmediato, solo ir a investigar
            navAgent.isStopped = false;
            navAgent.SetDestination(soundPosition);
            enemyAnimations.Walk();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}