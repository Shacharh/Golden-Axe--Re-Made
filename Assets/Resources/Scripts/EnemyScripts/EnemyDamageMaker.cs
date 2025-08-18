using UnityEngine;

public class EnemyDamageMaker : MonoBehaviour
{
    public float enemyDamage = 20f;
    private Health playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth = other.GetComponent<Health>();
            playerHealth.TakeDamage(enemyDamage);
            Debug.Log("enemyMadeDamage"+ other.name);

        }

    }

}
