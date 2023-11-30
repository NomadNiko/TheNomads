using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public float damage = 10f; // Damage inflicted by the fire
    public float duration = 10f; // Duration the fire lasts
    public float burnDuration = 3f;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        AddBurnStatus(other);
    }

    private void OnTriggerStay(Collider other)
    {
        AddBurnStatus(other);
    }

    private void AddBurnStatus(Collider victim)
    {
        if (victim.CompareTag("Enemy") || (victim.CompareTag("Player"))) // hits enemies and player
        {
            OnFireStatus status = victim.GetComponent<OnFireStatus>();
            if (status == null)
            {
                status = victim.gameObject.AddComponent<OnFireStatus>();
            }
            
            status.RefreshBurning(damage, burnDuration);
        }
    }
}
