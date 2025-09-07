using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{

    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float health;
    public float damageRecieved;
    private float lerpSpeed = 0.05f;

    public event System.Action onDeath;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }

        healthSlider.transform.forward = Camera.main.transform.forward;
        easeHealthSlider.transform.forward = Camera.main.transform.forward;
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            EnemyTakeDamage(damageRecieved);
            Debug.Log("enemyTookDamage");
        }
        
    }*/

    public void EnemyTakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
