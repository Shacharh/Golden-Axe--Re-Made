using Unity.Cinemachine;
using UnityEngine;

public class CameraFOVChanger : MonoBehaviour
{
    [SerializeField] float fov = 65;
    [SerializeField] CinemachineCamera cameraObject;
    [SerializeField] private float transitionTime = 1;

    bool entered = false;
    float initialFOV;
    float timer;


    private void Update()
    {
        if (entered)
        {
            timer += Time.deltaTime;

            float newFOV = Mathf.Lerp(initialFOV, fov, timer / transitionTime);

            cameraObject.Lens.FieldOfView = newFOV;
            if (timer >= transitionTime)
            {
                entered = false;
                timer = 0;
                cameraObject.Lens.FieldOfView = fov;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            entered = true;
            initialFOV = cameraObject.Lens.FieldOfView;
        }
    }
}