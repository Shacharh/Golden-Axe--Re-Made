using System;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorCall : MonoBehaviour
{
    [SerializeField] PlayableDirector dir;
    private void OnTriggerEnter(Collider other)
    {
        dir.Play();
    }
}
