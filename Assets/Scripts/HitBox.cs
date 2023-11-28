using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool canDamage;
    public bool isComplete;
    // Start is called before the first frame update
    void Start()
    {
        isComplete = true; //This has to be defined here, if defined before start it doesnt work
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //TODO: Make this do damage when we have a healtth setup
    private void OnTriggerEnter(Collider collider){
        if (canDamage && collider.gameObject.tag=="Player") {
            Debug.Log ("hitplayer");
            canDamage = false;
        }
        
    }

    //Event function for when hitbox animation completes, primarily used for locking movement so far 
    //Must be int, bollean does not work with animation events
    private void AnimStatus(int status){
        isComplete = (status == 1); //Please enlighten me on a better way to do this
    }
}
