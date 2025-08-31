using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem ps;

    public void PlayParticles()
    {
        if (ps != null)
            ps.Play();
    }
}