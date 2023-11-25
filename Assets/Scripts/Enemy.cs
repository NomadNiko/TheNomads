using UnityEngine;

public class Enemy : MonoBehaviour {
    private Animator animator;
    private Rigidbody rb;
    private Transform playerTransform;
    private float moveSpeed = 3f;
    private float rotateSpeed = 200f; // The speed at which the enemy rotates to face the player

    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate() {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Vector3 nextPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        rb.MovePosition(nextPosition);

        // If the enemy is moving, set the walking animation
        animator.SetBool(IsWalkingHash, rb.velocity.magnitude > 0);

        // Rotate the enemy to face towards the player
        if (rb.velocity != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
