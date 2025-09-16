using UnityEngine;

public class CameraSideLight : MonoBehaviour
{
    public Transform player;   // Drag  player here
    public Transform cam;      // Drag  main camera here
    public float distance = 2f; // Distance of the light from the player

    void LateUpdate()
    {
        if (player == null || cam == null) return;

        // Direction from player to camera
        Vector3 dirToCam = (cam.position - player.position).normalized;

        // Place the light at player's position + direction towards camera
        transform.position = player.position + dirToCam * distance;

        // Make the light look at the player (optional, looks more natural)
        transform.LookAt(player.position);
    }
}