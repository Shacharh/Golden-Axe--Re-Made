using UnityEngine;

public class CombatSystem : MonoBehaviour
{

    private Animator anim;

    [Header("Attack Settings")]
    public float cooldownTime = 2f;
    private float nextHitTime = 0f;
    public static int numOfClicks = 0;
    float lastClickedTime = 0f;
    float maxComboDelay = 1f;

    [Header("Weapon Settings")]
    [SerializeField] BoxCollider weaponCollider;
    public float weaponDamage = 20f;
    private bool hasHit = false; //one hit per swing check
   
    void Start()
    {
        anim = GetComponent<Animator>();
        weaponCollider.enabled = false;

    }

    void Update()
    {
        //reset combo if too much time has passed
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numOfClicks = 0;
        }

        //handle attack input
        if (Time.time > nextHitTime)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.G))
            {
                OnClick();
            }
        }

        //disable attack anims bools after half anim time
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack1")) anim.SetBool("attack1", false);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack2")) anim.SetBool("attack2", false);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
            {
                anim.SetBool("attack3", false);
                numOfClicks = 0;
            }
        }

        //enabe/disable weapon collider based on anim state
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("attack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            if(!weaponCollider.enabled)
            {
                weaponCollider.enabled = true;
                hasHit = false; //reset hit state at the start of swing
            }
        }

        else
        {
            weaponCollider.enabled = false;
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        numOfClicks++;
        numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);
        if (numOfClicks == 1)
        {
            anim.SetBool("attack1", true);
        }

        if(numOfClicks >=2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            anim.SetBool("attack1", false);
            anim.SetBool("attack2", true);
        }

        if (numOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
        {
            anim.SetBool("attack2", false);
            anim.SetBool("attack3", true);
        }

    }

    //Weapon damage making 
    private void OnTriggerEnter(Collider other)
    {
        if (!weaponCollider.enabled || hasHit) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.EnemyTakeDamage(weaponDamage);
                hasHit = true; //prevents multiple hits in the same animation
                Debug.Log("hit");
            }
        }
    }
}
