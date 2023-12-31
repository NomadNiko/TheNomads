using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    [SerializeField] private Rigidbody firework;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject player;

    private float rotationSpeed = 1f;
    private float moveSpeed = 4f;

    private GameObject pan;
    private Animation anim;
    private HitBox panHitBox;
    private Health health; // Add reference to Health script

    private float travelTime = 5.0f;
    private float attackCd = 3.0f;
    private float lastAttack = 0.0f;
    private bool movementLock = false;

    void Start() {
        pan = GameObject.Find("Wok");
        panHitBox = pan.GetComponent<HitBox>();
        anim = pan.GetComponent<Animation>();

        // Initialize health reference
        health = GetComponent<Health>();
    }

    void FixedUpdate() {
        if (!movementLock && player != null) {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 20) {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }

        if (Time.time - lastAttack > attackCd && player != null) {
            int chosenAttack = Random.Range(0, 4);
            if (chosenAttack == 0) {
                ShootFirework(player.transform.position);
            }else if (chosenAttack == 1){
                for (int i=0; i < 6; i++){ //Shoot 6 fireworks in a circle around the player, at a range of 20. TODO: Unhardcode these
                    Vector3 attackVector = transform.position + Quaternion.Euler(0,60*i,0)*Vector3.forward*20;
                    attackVector.y = player.transform.position.y;
                    ShootFirework(attackVector);
                }
            }else {
                PanSlam();
            }
            lastAttack = Time.time;
        }

        movementLock = !panHitBox.isComplete;

        // Check boss health and destroy if it's zero
        if (health.GetHealth() <= 0) {
            Destroy(gameObject);
        }

        movementLock = !panHitBox.isComplete;

    }

    void ShootFirework(Vector3 target) {
        Rigidbody projectile = Instantiate(firework, transform.position + new Vector3(0,3,0), transform.rotation);
        GameObject tempWarning = Instantiate(warning, target + new Vector3(0, 0.1f, 0), transform.rotation);
        projectile.gameObject.GetComponent<FireworkAttack>().warning = tempWarning;

        Vector3 travelVector = target - transform.position;

        projectile.velocity = new Vector3(travelVector[0] / travelTime,
                                          (travelVector[1] + 4.9f * travelTime * travelTime) / travelTime,
                                          travelVector[2] / travelTime);

        // Optionally, you can apply damage to the boss here for testing purposes
        // health.TakeDamage(damageAmount);
    }

    void PanSlam() {
        //TODO: Make the boss stop during the telegraph and make it faster
        anim.Play("PanSlam");

        panHitBox.canDamage = true;
    }
}

