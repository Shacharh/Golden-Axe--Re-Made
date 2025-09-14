using UnityEngine;

public class EnemyDamageMaker : MonoBehaviour
{
    public float enemyDamage = 20f;
    public Health playerHealth;
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
        if (other.gameObject.tag == "Player")
        {
            playerHealth = other.GetComponent<Health>();
            playerHealth.TakeDamage(enemyDamage);
            Debug.Log("player got hit" + enemyDamage);
        }

    }

}
