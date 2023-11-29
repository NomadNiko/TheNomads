using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   public void OnTriggerEnter(Collider other)
    {
        // Destroy the projectile when it collides with any object
        Destroy(gameObject);
    }
}