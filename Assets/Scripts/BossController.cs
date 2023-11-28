using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
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
    private float panAttackDamage = 20f;
    private bool movementLock = false;

    void Start() {
        pan = GameObject.Find("Pan");
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
            int chosenAttack = Random.Range(0, 2);
            if (chosenAttack == 1) {
                ShootFirework(player.transform.position);
            } else {
                StartCoroutine(PanSlamCoroutine());
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
        projectile.gameObject.GetComponent<FireworkAttack>().warning = tempWarning;

        Vector3 travelVector = target - transform.position;

        projectile.velocity = new Vector3(travelVector[0] / travelTime,
                                          (travelVector[1] + 4.9f * travelTime * travelTime) / travelTime,
                                          travelVector[2] / travelTime);

        // Optionally, you can apply damage to the boss here for testing purposes
        // health.TakeDamage(damageAmount);
    }

    IEnumerator PanSlamCoroutine() {
        anim.Play("PanSlam");

        // Wait for X seconds before applying damage
        yield return new WaitForSeconds(1.5f);

        // Check if the player has a Health component
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null) {
            // Apply damage to the player
            playerHealth.TakeDamage(panAttackDamage); // Adjust the damage amount as needed
        }

        panHitBox.canDamage = true;
    }
}

=======
public class BossController : MonoBehaviour
{

    // Declare associated gameobjects
    [SerializeField] private Rigidbody firework;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject player;
    //Movement properites
    private float rotationSpeed = 0.5f;

    //Pan Slam Properties
    private GameObject pan;
    private Animation anim;

    //Firework Properties
    private float travelTime = 5.0f;
    //Attacking Properties
    private float attackCd = 3.0f;
    private float lastAttack = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        pan = GameObject.Find("Pan");
        anim = pan.GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        //Always look at player without tilting, TODO make it so it pauses in it during other attacks
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0, player.transform.position.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*rotationSpeed);

        //Attack every x seconds, can update in the future if there is a better practice
        //TODO: Add more variation, maybe make fireworks also a pattern where we shoot 4 or 5
        if (Time.time - lastAttack > attackCd){
            int chosenAttack = Random.Range(0,2);
            if (chosenAttack == 1){
                ShootFirework(player.transform.position);
            } else {
                PanSlam();
            }
            lastAttack = Time.time;
        }
    }

    //Shoot firework at specified target position
    void ShootFirework(Vector3 target){
        //Create instances of prefabs
        Rigidbody projectile = Instantiate(firework, transform.position, transform.rotation);
        GameObject tempWarning = Instantiate(warning, target + new Vector3(0,0.1f,0), transform.rotation); //Raise the warning slightly to stop interference with the ground
        projectile.gameObject.GetComponent<FireWork>().warning = tempWarning;
        
        //Determine the position vector from the origin to target
        Vector3 travelVector = target - transform.position;

        //Set the firework velocity, if you want the math justified feel free to ask me (Matti) 
        projectile.velocity = new Vector3(travelVector[0]/travelTime, 
                                            (travelVector[1]+4.9f*travelTime*travelTime)/travelTime, 
                                            travelVector[2]/travelTime);
    }

    //Start pan slam animation, maybe not the best way to do this, open to redoing it if wanted
    void PanSlam(){
        anim.Play("PanSlam");
        pan.GetComponent<HitBox>().canDamage = true;
    }
}
>>>>>>> origin/dev
