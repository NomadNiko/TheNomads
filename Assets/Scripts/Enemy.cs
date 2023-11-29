using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Enemy related methods and properties
    //Navigation

    public Transform player;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.destination = player.position;
    }

    public void Flatten()
    {
        //Debug.Log("has been flattened");
        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        // Code to flatten the enemy
    }
}
