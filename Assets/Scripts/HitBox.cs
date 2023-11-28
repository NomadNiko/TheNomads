using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {
    public bool canDamage;
    public bool isComplete;
    public float explosionDamage = 20; // Added variable for explosion damage

    // Start is called before the first frame update
    void Start() {
        isComplete = true;
    }

    // Update is called once per frame
    void Update() {

    }

    // TODO: Make this do damage when we have health setup
    private void OnTriggerEnter(Collider collider) {
        if (canDamage && collider.gameObject.CompareTag("Player")) {
            // Assuming the player has a Health component
            Health playerHealth = collider.gameObject.GetComponent<Health>();

            if (playerHealth != null) {
                // Apply damage to the player
                playerHealth.TakeDamage(explosionDamage);

                // For debugging, print the damage applied
                Debug.Log($"Player took damage: {explosionDamage}");
            } else {
                Debug.LogWarning("Player object does not have a Health component.");
            }

            canDamage = false;
        }
    }

    // Event function for when the hitbox animation completes, primarily used for locking movement so far 
    // Must be int, boolean does not work with animation events
    private void AnimStatus(int status) {
        isComplete = (status == 1);
    }
}
