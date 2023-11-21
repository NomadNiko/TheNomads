using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float travelTime = 2.0f;
    //Attacking Properties
    private float attackCd = 3.0f;
    private float lastAttack = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        pan = GameObject.Find("Pan");
        anim = pan.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
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

    void FixedUpdate()
    {
        
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
        pan.GetComponent<PanHitBox>().canDamage = true;
    }
}
