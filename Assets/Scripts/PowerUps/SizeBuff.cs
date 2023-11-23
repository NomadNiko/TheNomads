using System.Collections;
using UnityEngine;

namespace PowerUps
{
    public class PowerUp : MonoBehaviour
    {
        public float powerUpDuration = 4;
        public float sizeMultiplier = 1.7f;
        public GameObject pickupEffect;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Pickup(other));
            }
        }

        IEnumerator Pickup(Collider player)
        {
            //For Pickup Particle Effects
            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            //Effect Of PowerUp
            player.transform.localScale *= sizeMultiplier;
            
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(powerUpDuration);

            player.transform.localScale /= sizeMultiplier;

            Destroy(gameObject);
            Debug.Log("Picked Up");
        }
    }
}

