using System.Collections;
using UnityEngine;

namespace PowerUps
{
    public class SizeBuff : MonoBehaviour
    {
        public float powerUpDuration = 4;
        public float sizeMultiplier = 1.7f;
        public bool hasSizeBuff;
        public GameObject pickupEffect;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerCollisionHandler playerHandler = other.GetComponent<PlayerCollisionHandler>();
                if (playerHandler != null)
                {
                    playerHandler.SetSizeBuff(true);
                }
                //Debug.Log("Picked Up");
                StartCoroutine(Pickup(other));
            }
        }

        IEnumerator Pickup(Collider player)
        {
            //For Pickup Particle Effects
            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            //Effect Of PowerUp
            player.transform.localScale *= sizeMultiplier;
            hasSizeBuff = true;
            
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(powerUpDuration);

            player.transform.localScale /= sizeMultiplier;
            hasSizeBuff = false;

            Destroy(gameObject);
        }
        
    }
}

