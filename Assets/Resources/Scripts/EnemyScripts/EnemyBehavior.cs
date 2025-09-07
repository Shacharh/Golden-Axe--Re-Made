using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("PlayerDetection")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private bool showDebugVisuals = true;
    [SerializeField] private LayerMask playerMask;
    public GameObject LastTarget { get; private set; }

    [Header("Combat")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    [Header("chaseMemory")]
    [SerializeField] private float memoryDuration = 5f;
    private float memoryTimer;
    private bool chasingLastPosition;
    private Vector3 lastKnownLocation;

    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.stoppingDistance = attackRange * 0.9f;
    }

    private void Update()
    {
        DetectTarget();
        
        if (LastTarget != null)
        {
            float distance = Vector3.Distance(transform.position, LastTarget.transform.position);

            if (distance > attackRange)
            {
                //chase player
                agent.isStopped = false;
                agent.SetDestination(LastTarget.transform.position);

                lastKnownLocation = LastTarget.transform.position;
                memoryTimer = memoryDuration;
                chasingLastPosition = true;

                animator.SetBool("Run", true);
                //animator.SetBool("IsAttacking", false);
            }
            
            else
            {
                //reached player, sets attack animation and remembering player location
                agent.isStopped = true;
                animator.SetBool("Run", false);

                if(Time.time - lastAttackTime >= attackCooldown)
                {
                    animator.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }

                /*else
                {
                    animator.SetBool("IsAttacking", false);
                    lastKnownLocation = LastTarget.transform.position;
                    memoryTimer = memoryDuration;
                    chasingLastPosition = true;
                }*/
            }
        }

        else if(chasingLastPosition)
        {
            //player out of range, start chasing player again
            agent.isStopped = false;
            agent.SetDestination(lastKnownLocation);

            animator.SetBool("Run", true);
            //animator.SetBool("IsAttacking", false);

            memoryTimer -= Time.deltaTime;

            if(memoryTimer <= 0f || Vector3.Distance(transform.position, lastKnownLocation) < 0.5f)
            {
                agent.isStopped = true;
                animator.SetBool("Run", false);
                chasingLastPosition = false;
            }
        }
        
        else
        {
            //lost player, back to idle
            agent.isStopped = true;
            animator.SetBool("Run", false);
        }

    }

    public void DetectTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerMask);
        LastTarget = hits.Length > 0 ? hits[0].gameObject : null;
        
    }


    private void OnDrawGizmos()
    {
        if (!showDebugVisuals) return;
        Gizmos.color = LastTarget ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
