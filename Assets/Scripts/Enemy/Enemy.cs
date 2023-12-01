using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (player != null) {
            _agent.destination = player.position;
        }
    }

    private void Update()
    {
        if (player != null) {
            _agent.destination = player.position;
        }
    }

    public void SetTarget(Transform newTarget) {
        player = newTarget;
        if (_agent != null) {
            _agent.destination = player.position;
        }
    }
    
    public void Flatten()
    {
        //Debug.Log("has been flattened");
        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        // Code to flatten the enemy
    }
}
