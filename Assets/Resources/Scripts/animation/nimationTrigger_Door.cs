using UnityEngine;
using UnityEngine.Serialization;

public class nimationTrigger_Door : MonoBehaviour
{
    // Reference to the Animator component
    public Animator animator;

    // Name of the animation trigger in the Animator
    public string openDoor = "Play";
    public string closeDoor = "Close";
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Trigger the animation
            animator.SetTrigger(openDoor);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Trigger the animation
            animator.SetTrigger(closeDoor);
        }
    }
    
}
