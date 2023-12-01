using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScaleUI : MonoBehaviour
{
    [SerializeField] private Scales scales;
    [SerializeField] private TextMeshProUGUI scaleCount;

    // Update is called once per frame
    void Update()
    {
        scaleCount.text = scales.scales.ToString();
    }
}
