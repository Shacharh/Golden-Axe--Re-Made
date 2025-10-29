using UnityEngine;
using System.Collections.Generic;
using StarterAssets;

public class NewCombatSystem : MonoBehaviour
{
    private Animator anim;

    [Header("Attack Settings")]
    public float comboResetTime = 1f; // reset combo if no input after X seconds
    private int currentComboStep = 0;
    private float comboTimer = 0f;
    private bool attackInProgress = false;
    private bool inputBuffered = false;

    [System.Serializable]
    public class AttackData
    {
        public string animationTrigger; // trigger name in Animator
        public float attackDuration = 0.8f; // total time attack takes
        public float inputBufferTime = 0.5f; // when next input is allowed
        public float damage = 20f;
    }

    [Header("Combo Settings")]
    public List<AttackData> comboAttacks = new List<AttackData>();

    [Header("Weapon Settings")]
    [SerializeField] BoxCollider weaponCollider;

    private HashSet<GameObject> enemiesHit = new HashSet<GameObject>();
    private ThirdPersonController playerController;
    private float originalSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        weaponCollider.enabled = false;

        playerController = GetComponent<ThirdPersonController>();
        if (playerController != null)
        {
            originalSpeed = playerController.MoveSpeed;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.G))
        {
            if (attackInProgress)
            {
                // buffer input if player presses too early
                inputBuffered = true;
            }
            else
            {
                StartAttack(0);
            }
        }

        if (attackInProgress)
        {
            comboTimer -= Time.deltaTime;

            // Check if we can queue next attack
            if (comboTimer <= comboAttacks[currentComboStep].attackDuration - comboAttacks[currentComboStep].inputBufferTime)
            {
                if (inputBuffered && currentComboStep + 1 < comboAttacks.Count)
                {
                    inputBuffered = false;
                    StartAttack(currentComboStep + 1);
                }
            }

            // End attack if timer runs out
            if (comboTimer <= 0f)
            {
                attackInProgress = false;
                currentComboStep = 0;
                
                if(playerController != null)
                    playerController.MoveSpeed = originalSpeed;
            }
        }
    }

    void StartAttack(int step)
    {
        currentComboStep = step;
        var attack = comboAttacks[step];
        attackInProgress = true;
        comboTimer = attack.attackDuration;
        inputBuffered = false;

        anim.SetTrigger(attack.animationTrigger);
        Debug.Log("Attack " + (step + 1));
        
        if(playerController != null)
            playerController.MoveSpeed = 0;
    }

    // === Weapon Damage ===
    public void OnWeaponTriggerEnter(Collider other)
    {
        if (!weaponCollider.enabled) return;
        if (!other.CompareTag("Enemy")) return;
        if (enemiesHit.Contains(other.gameObject)) return;
        Debug.Log("Enemy Detected");

        var enemy = other.GetComponent<HealthSystem>();
        if (enemy != null)
        {
            enemy.TakeDamage(comboAttacks[currentComboStep].damage);
            enemiesHit.Add(other.gameObject);
            Debug.Log("enemu got hit");
        }
    }

    // Called by Animation Events
    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        enemiesHit.Clear();
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        enemiesHit.Clear();
    }
}
