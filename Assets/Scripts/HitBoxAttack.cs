using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{

    private bool canAttack;

    private void OnEnable(){
        canAttack = true;
    }

    //TODO: Pull damage from player, will change per attack in the combo, and deal damage to what is collided with
    private void OnTriggerEnter(Collider other){
        if (canAttack) {
            Debug.Log ("hit");
        }
        canAttack = false;
    }
}
