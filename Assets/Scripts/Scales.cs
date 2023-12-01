using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : MonoBehaviour
{
    public int scales;
    // Start is called before the first frame update
    public void AddScales(int amount)
    {
        scales += amount;
    }

    private void Update()
    {
        //Debug.Log("Scales: " + scales);
    }
}
