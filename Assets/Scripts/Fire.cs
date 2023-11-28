using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public float damage = 10f; // Damage inflicted by the fire
    public float radius = 5f;  // Radius of the fire's effect
    public float duration = 3f; // Duration the fire lasts

    private void Start()
    {
        fireParticles.Play();
        InvokeRepeating("DamageEnemies", 0f, 1f); // Damage every second
        Destroy(gameObject, duration); // Destroy fire effect after duration
    }

    private void DamageEnemies()
    {
        // Find all colliders within the radius of the fire
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) // Assuming enemies are tagged "Enemy"
            {
                // Apply damage to each enemy within the radius
                
            }
        }
    }
}