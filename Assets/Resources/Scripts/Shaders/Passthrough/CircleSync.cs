using Unity.Cinemachine;
using UnityEngine;


public class CircleSync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");
    public Material WallMaterial;
    public Camera Camera;
    public LayerMask Mask;
    public float Radius = 1;
    public bool rayhit = false;
    
    private void Update()
    {
        var Dir = Camera.transform.position - transform.position;
        var ray = new Ray(Camera.transform.position, Dir.normalized);

        if (Physics.Raycast(ray, Mathf.Infinity, Mask))
        {
            WallMaterial.SetFloat(SizeID, Radius);
            rayhit = true;
        }
        else
        {
            WallMaterial.SetFloat(SizeID, 0);
            rayhit = false;
        }
        
        var view = Camera.WorldToViewportPoint(transform.position);
        WallMaterial.SetVector(PosID, view);
    }
}
