using UnityEngine;

public class ParticlesRigidbodyStabilizer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        if (transform.up.y == -1)
        {
            _particleSystem.gravityModifier = -_particleSystem.gravityModifier;
        }
    }
}
