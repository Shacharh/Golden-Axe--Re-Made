using UnityEngine;


public class CircleSync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");
    public Material WallMaterial;
    public Camera Camera;
    public LayerMask Mask;
    public float Radius = 1;
    void Update()
    {
        var Dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, Dir.normalized);

        if (Physics.Raycast(ray, Mathf.Infinity, Mask))
        {
            WallMaterial.SetFloat(SizeID, Radius);
        }
        else
        {
            WallMaterial.SetFloat(SizeID, 0);
        }
        
        var view = Camera.WorldToViewportPoint(transform.position);
        WallMaterial.SetVector(PosID, view);
    }
}
