using System.Collections;
using UnityEngine;

namespace PowerUps
{
    public class SpeedBuff : MonoBehaviour
    {
        public float powerUpDuration = 4;
        public float speedMultiplier = 1.6f;
        public bool hasSpeedBoost = false;
        public GameObject pickupEffect;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Debug.Log("Picked Up");
                StartCoroutine(Pickup(other));
            }
        }

        IEnumerator Pickup(Collider player)
        {
            //For Pickup Particle Effects
            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            //Effect Of PowerUp
            CharacterMovement playerMovement = player.GetComponent<CharacterMovement>();
            playerMovement.moveSpeed *= speedMultiplier;
            hasSpeedBoost = true;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(powerUpDuration);

            playerMovement.moveSpeed /= speedMultiplier;
            hasSpeedBoost = false;
            
            Destroy(gameObject);
        }
    }
}