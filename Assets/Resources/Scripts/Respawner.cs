using UnityEngine;

public class Respawner : MonoBehaviour
{
    public string playerTag = "Player";
    public Transform respawnLocation;
    public Transform playerLocation;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            ResetPlayerLocation();
            
        }
    }

    

    private void ResetPlayerLocation()
    {
        playerLocation.position = respawnLocation.position;
        Physics.SyncTransforms();
    }

    public void TeleportPlayer(Transform newPosition)
    {
        playerLocation.position = newPosition.position;
    }
}
