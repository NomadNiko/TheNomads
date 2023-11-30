using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    public float damage = 10f; // The amount of damage this object will deal to enemies

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the object is moving fast enough (i.e., has been thrown), check for enemy collision
        if (_rb.velocity.magnitude > 1f && collision.gameObject.CompareTag("Enemy"))
        {
            // Try to get the Health component on the enemy
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damage);
            }

            // Optionally, handle the object's destruction or disable it after dealing damage
            // Destroy(gameObject); // Uncomment this if the object should be destroyed on impact
        }
    }
}