using UnityEngine;

public class Enemy : MonoBehaviour {
    private Health healthComponent;
    private Animator animator;
    private Rigidbody rb;
    private Transform playerTransform;
    private float closeEnoughDistance = 1f;
    private float moveSpeed = 3f;

    private static readonly int IsAttackingHash = Animator.StringToHash("isAttacking");

    private void Awake() {
        healthComponent = gameObject.GetComponent<Health>();
        if (healthComponent == null) {
            Debug.LogWarning("Health component not found on Enemy GameObject. Trying to find it in children.");
            healthComponent = GetComponentInChildren<Health>();

            if (healthComponent == null) {
                Debug.LogError("Health component not found in children. Make sure it is attached to the same GameObject as the Enemy script.");
            }
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform == null) {
            Debug.LogError("Player not found. Make sure the player is in the scene and tagged as 'Player'.");
        }
    }


    private void Update() {

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > closeEnoughDistance) {
            Debug.Log("Moving towards player. Distance: " + distanceToPlayer);
            animator.SetBool(IsAttackingHash, false);
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        } else {
            Debug.Log("Player is close. Stopping and attacking.");
            rb.velocity = Vector3.zero;
            animator.SetBool(IsAttackingHash, true);
        }
    }

    private void Start() {
        // Attach the Health component to the Enemy
        healthComponent = gameObject.GetComponent<Health>();

        if (healthComponent == null) {
            // If Health component is not found, log a warning
            Debug.LogWarning("Health component not found on Enemy GameObject.");
        }
    }

    // Example method to damage the enemy
    public void DamageEnemy(float damageAmount = 25) {
        if (healthComponent != null) {
            healthComponent.TakeDamage(damageAmount);
        } else {
            // Log a warning if the Health component is not available
            Debug.LogWarning("Health component not available. Unable to damage enemy.");
        }
    }

    // Example method to heal the enemy
    public void HealEnemy(float healAmount = 25) {
        if (healthComponent != null) {
            healthComponent.Heal(healAmount);
        } else {
            // Log a warning if the Health component is not available
            Debug.LogWarning("Health component not available. Unable to heal enemy.");
        }
    }
}
