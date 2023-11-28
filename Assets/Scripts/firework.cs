using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWork : MonoBehaviour
{
    // Start is called before the first frame update

    // Properties, feel free to change as we balance
    public float damage = 10;
    //private Rigidbody rb;
    public GameObject warning; 
    public GameObject explosion;

    private GameObject explosionHitBox;

    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Always point firework in direction of movement. Might need to change when a model gets added
        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
    }

    void OnTriggerEnter(Collider collider)
    {
        //Instantiate(explosion,transform.position,transform.rotation);
        if (collider.gameObject.tag == "Ground"){
            explosionHitBox = Instantiate(explosion, transform.position, transform.rotation);
            explosionHitBox.gameObject.GetComponent<HitBox>().canDamage = true;
            Invoke("EndExplosion", 0.1f);
            warning.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }

    void EndExplosion(){
        Destroy(explosionHitBox);
        DestroySelf();
    }
    //This is done to allow the explosion radius destruction to be handled in this script, allowing the hitbox script to be reused.
    void DestroySelf(){
        Destroy(warning);
        Destroy(gameObject);
    }
}
