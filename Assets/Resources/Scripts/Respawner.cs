using UnityEngine;

public class Respawner : MonoBehaviour
{
    public string playerTag = "Player";
    public Transform respawnLocation;
    public Transform playerLocation;
    [SerializeField] Health playerHealth;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            ResetPlayerLocation();
            playerHealth.TakeDamage(50);
            
        }
    }

    

    private void ResetPlayerLocation()
    {
        playerLocation.position = respawnLocation.position;
        Physics.SyncTransforms();
    }

    
}
