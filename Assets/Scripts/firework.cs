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

    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //Instantiate(explosion,transform.position,transform.rotation);
        if (collision.gameObject.tag == "Ground"){
            Destroy(warning);
            Destroy(gameObject);
            
        }
        //Debug.Log(transform.position);
    }
}
