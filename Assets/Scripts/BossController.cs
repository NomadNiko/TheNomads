using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    [SerializeField] private Rigidbody firework;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject player;

    private float rotationSpeed = 1f;
    private float moveSpeed = 2.2f;

    private GameObject pan;
    private Animation anim;
    private HitBox panHitBox;
    private Health health; // Add reference to Health script

    private float travelTime = 5.0f;
    private float attackCd = 3.0f;
    private float lastAttack = 0.0f;
    private bool movementLock = false;

    void Start() {
        pan = GameObject.Find("Pan");
        panHitBox = pan.GetComponent<HitBox>();
        anim = pan.GetComponent<Animation>();

        // Initialize health reference
        health = GetComponent<Health>();
    }

    void FixedUpdate() {
        if (!movementLock) {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 20) {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }

        if (Time.time - lastAttack > attackCd) {
            int chosenAttack = Random.Range(0, 2);
            if (chosenAttack == 1) {
                ShootFirework(player.transform.position);
            } else {
                PanSlam();
            }
            lastAttack = Time.time;
        }

        movementLock = !panHitBox.isComplete;

        // Check boss health and destroy if it's zero
        if (health.currentHealth <= 0) {
            Destroy(gameObject);
        }

        movementLock = !panHitBox.isComplete;

    }

    void ShootFirework(Vector3 target) {
        Rigidbody projectile = Instantiate(firework, transform.position, transform.rotation);
        GameObject tempWarning = Instantiate(warning, target + new Vector3(0, 0.1f, 0), transform.rotation);
        projectile.gameObject.GetComponent<FireWork>().warning = tempWarning;

        Vector3 travelVector = target - transform.position;

        projectile.velocity = new Vector3(travelVector[0] / travelTime,
                                          (travelVector[1] + 4.9f * travelTime * travelTime) / travelTime,
                                          travelVector[2] / travelTime);

        // Optionally, you can apply damage to the boss here for testing purposes
        // health.TakeDamage(damageAmount);
    }

    void PanSlam() {
        anim.Play("PanSlam");
        panHitBox.canDamage = true;
    }
}
