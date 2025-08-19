using UnityEngine;

public class CombatSystem : MonoBehaviour
{

    private Animator anim;
    public float cooldownTime = 2f;
    private float nextHitTime = 0f;
    public static int numOfClicks = 0;
    float lastClickedTime = 0f;
    float maxComboDelay = 1f;

    [SerializeField] BoxCollider weaponCollider;
   
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            anim.SetBool("attack1", false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
        {
            anim.SetBool("attack2", false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            anim.SetBool("attack3", false);
            numOfClicks = 0;

        }

        if(Time.time - lastClickedTime > maxComboDelay)
        {
            numOfClicks = 0;
        }

        if(Time.time > nextHitTime)
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("attack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            weaponCollider.enabled = true;
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
        if(numOfClicks == 1)
        {
            anim.SetBool("attack1", true);
        }
        numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);

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
}
