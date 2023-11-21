using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanHitBox : MonoBehaviour
{
    public bool canDamage;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
