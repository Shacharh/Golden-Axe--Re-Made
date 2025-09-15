using UnityEngine;

public class WeaponBehaviourProxy : MonoBehaviour
{
    private NewCombatSystem owner;

    void Awake()
    {
        owner = GetComponentInParent<NewCombatSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy");
        owner?.OnWeaponTriggerEnter(other);
    }
}
