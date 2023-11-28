using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour {
    private bool canAttack;
    public int comboNum;
    [SerializeField] private float[] damage = new float[3] { 10, 10, 15 };

    private Health health; // Add reference to Health script

    private void OnEnable() {
        canAttack = true;

        // Initialize health reference
        health = GetComponentInParent<Health>();
    }

    private void OnTriggerEnter(Collider collider) {
        if (canAttack) {
            Debug.Log("hit");

            // TODO: Replace this with the actual damage value
            float damageAmount = damage[comboNum];
            health.TakeDamage(damageAmount);
        }
        canAttack = false;
    }
}
