using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PotionPickUp : MonoBehaviour
{
 
    Health _health;
    Mana _mana; // script not in game yet
    public enum PotionType
    {
    Mana, HP
    }
    public PotionType potionType;
    [SerializeField] int addAmount;



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //identify if Player entered collider 
        if(other.CompareTag("Player"))
        {
            // if HP/mana, depending on enum bar is full, return
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
                     break;
                 case PotionType.Mana:
                    _mana.AddMana(addAmount);
                    Destroy(gameObject);
                     Debug.Log("Mana added");
                     break;
             }
             
        
        }
       
            
    }

}
