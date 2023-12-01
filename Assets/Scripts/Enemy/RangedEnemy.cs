using UnityEngine;
using UnityEngine.AI;

public class ProjectileEnemy : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject projectilePrefab;
    public float firingRate = 2.0f;
    public float speed = 2.0f;
    public float heightOffset = 5.0f;
    

    private NavMeshAgent _agent;
    private float _lastFireTime;
    private const float SpawnOffset = 1.5f;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            playerTransform = player.transform;
        }
        _agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            bool clearLineOfSight = Physics.Raycast(transform.position, playerTransform.position - transform.position, out var hit) && hit.transform == playerTransform;

            if (distance <= _agent.stoppingDistance && clearLineOfSight)
            {
                // Stop and fire at the player
                _agent.isStopped = true;
                if (Time.time > _lastFireTime + firingRate)
                {
                    FireProjectile();
                    _lastFireTime = Time.time;
                }
            }
            else
            {
                // Keep chasing the player
                _agent.isStopped = false;
                _agent.SetDestination(playerTransform.position);
            }
        }
    }


    private void FireProjectile()
    {
        Vector3 spawnPosition = transform.position + transform.forward * SpawnOffset; //so it doesn't clash with the enemy collider
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Vector3 targetPosition = playerTransform.position + new Vector3(0, heightOffset, 0);
        Vector3 projectilePath = (targetPosition - transform.position).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = projectilePath * speed;
    }
}

