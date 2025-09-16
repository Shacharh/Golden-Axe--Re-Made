using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PotionPickUp : MonoBehaviour
{
 
    [SerializeField] HealthSystem _health;
    [SerializeField] Mana _mana; 
    public enum PotionType
    {
    Mana, HP
    }
    public PotionType potionType;
    [SerializeField] int addAmount;



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);
        Debug.Log("The tag of " + other.name + " is " + other.tag);

        //identify if Player entered collider 
        if(other.CompareTag("Player"))
        {
            
            Debug.Log("Something activated trigger: " + other.name);
            // if HP/mana, depending on enum bar is full, return
            Debug.Log("Potion activation detected");
            switch (potionType)
            {
                case PotionType.HP:
                    if (_health.health >= _health.maxHealth) //if health is full, return
                        return;
                    break;
                case PotionType.Mana:
                   if (_mana.mana >= _mana.maxMana) //if mana is full, return
                        return;
                   break;
            }
            

            
             // based on PotionType, figure out if need to add either Health, or mana to the player
             switch (potionType)
             {
                 case PotionType.HP: 
                     _health.AddHealth(addAmount); 
                     Debug.Log("HP added"); 
                     Destroy(gameObject);
                     Debug.Log("HP destroyed");
                     break;
                 case PotionType.Mana:
                    _mana.AddMana(addAmount);
                    Debug.Log("Mana added");
                    Destroy(gameObject);
                     Debug.Log("Mana destroyed");
                     break;
             }
             
        
        }
       
            
    }

}
