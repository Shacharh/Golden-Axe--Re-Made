using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float health;
    [SerializeField] Transform playerLocation;
    [SerializeField] Transform startingLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if(health <= 0)
        {
            Death();
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Death()
    {
        playerLocation = startingLocation;
        Physics.SyncTransforms();
        health = maxHealth;
    }
}
