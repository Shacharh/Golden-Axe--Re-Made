using UnityEngine;

public class AoeSystem : MonoBehaviour
{
    [Header("Charges Settings")]
    public int maxCharges = 3;
    public float[] damagePerCharge;
    public string[] attackAnimation;

    [Header("References")]
    public Animator animator;
    [SerializeField] EnemyHealth enemyHealth;

    private int currentCharges = 0;

    public void AddCharge()
    {
        currentCharges = Mathf.Clamp(currentCharges + 1, 0, maxCharges);
    }

    public void PerformAttack()
    {
        if (currentCharges <= 0) return;

        int index = Mathf.Clamp(currentCharges - 1, 0, attackAnimation.Length - 1);

        if(animator != null && attackAnimation.Length > 0)
        {
            animator.SetTrigger(attackAnimation[index]);
        }

        float damage = damagePerCharge[Mathf.Clamp(currentCharges - 1, 0, damagePerCharge.Length - 1)];

        enemyHealth.EnemyTakeDamage(damage);

        currentCharges = 0;
    }



}
