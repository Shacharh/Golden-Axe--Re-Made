using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private bool showDebugVisuals = true;
    [SerializeField] private LayerMask playerMask;
    public GameObject LastTarget { get; private set; }



    public GameObject DetectTarget()
    {
        var colliders = Physics.OverlapSphere(transform.position, detectionRadius, playerMask);
        if(colliders.Length > 0)
        {
            LastTarget = colliders[0].gameObject;
            
        }
        else
        {
            LastTarget = null;
        }
        return LastTarget;
    }

    private void OnDrawGizmos()
    {
        if (!showDebugVisuals) return;
        Gizmos.color = LastTarget ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
