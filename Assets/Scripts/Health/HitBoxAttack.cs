using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour {
    private bool canAttack;
    public int comboNum;
    [SerializeField] private float[] damage = new float[3] { 10, 10, 15 };

    private void OnEnable() {
        canAttack = true;
    }

    private void OnTriggerEnter(Collider collider) {
        if (canAttack) {

            float damageAmount = damage[comboNum];
            collider.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
        }
        canAttack = false;
    }
}
