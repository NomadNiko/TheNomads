using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{

    private bool canAttack;
    public int comboNum;
    [SerializeField] private float[] damage = new float[3] {10,10,15};

    private void OnEnable(){
        canAttack = true;
    }

    //TODO: Pull damage from player, will change per attack in the combo, and deal damage to what is collided with
    private void OnTriggerEnter(Collider collider){
        if (canAttack) {
            Debug.Log ("hit");
        }
        canAttack = false;
    }
}
