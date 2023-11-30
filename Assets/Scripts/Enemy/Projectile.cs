using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
    {
    
        public Health playerHealth;
        public float damage;

        private void Start()
        {
            //giving it health reference on instantiation
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            playerHealth.TakeDamage(damage);
            // Destroy the projectile when it collides with any object
            Destroy(gameObject);
        }
    }
