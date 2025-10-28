using UnityEngine;

public class EnemyDamageMaker : MonoBehaviour
{
    public float damage = 10f;
    [SerializeField] private BoxCollider weaponCollider;
    [SerializeField] private Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        //weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    //Called by Animtaion Event at the start of the swing
    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        
    }

    //Called by Animtaion Event at the end of the swing
    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<HealthSystem>();
            playerHealth.TakeDamage(damage);
            Debug.Log("player got hit" + damage);
        }

    }

}
