using UnityEngine;

public class EnemyDamageMaker : MonoBehaviour
{
    public float enemyDamage = 20f;
    private Health playerHealth;
    private Collider weaponCollider;
    [SerializeField] private Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        weaponCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if(enemyAnimator.GetBool("isAttacking"))
        {
            weaponCollider.enabled = true;
        }

        else
        {
            weaponCollider.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth = other.GetComponent<Health>();
            playerHealth.TakeDamage(enemyDamage);
        }

    }

}
