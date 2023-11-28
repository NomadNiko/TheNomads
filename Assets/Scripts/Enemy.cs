using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy related methods and properties
    public void Flatten()
    {
        //Debug.Log("has been flattened");
        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        // Code to flatten the enemy
    }
}
