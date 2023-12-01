using UnityEngine;

public class MeleeEnemy : MonoBehaviour {
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 2.0f;

    private Transform target;
    private float lastAttackTime;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has a "Player" tag
        lastAttackTime = -attackCooldown; // Initialize to allow immediate attack
    }

    void Update() {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        //Debug.Log("Distance to target: " + distanceToTarget); // Debug line

        if (distanceToTarget <= attackRange && Time.time >= lastAttackTime + attackCooldown) {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    void PerformAttack() {
        // Here you can add an animation for the attack
        Debug.Log("Enemy attacks!");

        // Assuming the player has a script with a method called 'TakeDamage'
        target.GetComponent<Health>().TakeDamage(attackDamage);
    }
}