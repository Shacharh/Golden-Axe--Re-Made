using UnityEngine;

public class WeaponBehaviourProxy : MonoBehaviour
{
    private CombatSystem owner;

    void Awake()
    {
        owner = GetComponentInParent<CombatSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        owner?.OnWeaponTriggerEnter(other);
    }
}
