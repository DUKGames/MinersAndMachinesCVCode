using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem particleSystem;


    public void SetParticleColor(Color color)
    {
        particleSystem.startColor = color;
    }

    public void PlayParticle()
    {
        particleSystem.Play();
    }

    public void StopParticle()
    {
        particleSystem.Stop();
    }

    public void SetParticlePosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
