using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float health = 0f;
    [SerializeField] private float lerpSpeed = 0.05f;

    [Header("Health UI")]
    public Slider healthBar;
    public Slider easeHealthBar;
    public bool worldSpaceHealthBar = false; //for use of enemy only

    public event Action onDeath;

    
    void Start()
    {
        health = maxHealth;
        if (healthBar != null) healthBar.value = maxHealth;
        if (easeHealthBar != null) easeHealthBar.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar != null && healthBar.value != health)
            healthBar.value = health;

        if (easeHealthBar != null && easeHealthBar.value != health)
            easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, health, lerpSpeed);

        if (worldSpaceHealthBar && healthBar != null)
            healthBar.transform.forward = Camera.main.transform.forward;
            easeHealthBar.transform.forward = Camera.main.transform.forward;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        onDeath?.Invoke();

        if (worldSpaceHealthBar)
            Destroy(gameObject);
        else
            Debug.Log("player died!");
    }

    public void AddHealth (float addAmount)
    {
        health += addAmount;
    }
}
