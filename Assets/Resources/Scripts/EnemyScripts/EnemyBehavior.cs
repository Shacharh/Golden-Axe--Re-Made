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
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    [Header("chaseMemory")]
    [SerializeField] private float memoryDuration = 5f;
    private float memoryTimer;
    private bool chasingLastPosition;
    private Vector3 lastKnownLocation;

    //private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DetectTarget();

       // agent.SetDestination(LastTarget.transform.position);
        
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
                animator.SetBool("isAttacking", false);
            }
            
            else
            {
                //reached player, sets attack animation and remembering player location
                agent.isStopped = true;
                animator.SetBool("Run", false);

                if(Time.time - lastAttackTime >= attackCooldown)
                {
                    animator.SetBool("isAttacking", true);
                    lastAttackTime = Time.time;
                }

                else
                {
                    lastKnownLocation = LastTarget.transform.position;
                    memoryTimer = memoryDuration;
                    chasingLastPosition = true;
                }
            }
        }

         else if(chasingLastPosition)
        {
            //player out of range, start chasing player again
            agent.isStopped = false;
            agent.SetDestination(lastKnownLocation);

            animator.SetBool("Run", true);
            animator.SetBool("isAttacking", false);

            memoryTimer -= Time.deltaTime;

            if(memoryTimer <= 0f || Vector3.Distance(transform.position, lastKnownLocation) < 0.5f)
            {
                agent.isStopped = true;
                animator.SetBool("Run", false);
                chasingLastPosition = false;
            }
        }
        
        
    }

    public void DetectTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerMask);
        if(hits.Length > 0)
        {
            LastTarget = hits[0].gameObject;
            Debug.Log(LastTarget.name);
            
        }
        else
        {
            LastTarget = null;
        }
        
    }


    private void OnDrawGizmos()
    {
        if (!showDebugVisuals) return;
        Gizmos.color = LastTarget ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
