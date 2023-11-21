using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public int itemCount = 4;
    public List<int> usedSlots = new List<int>();

    private float detectRange = 10.0f;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player; //Will need this to check proximity so only opens when we mean it to
    void Start()
    {
        for (int i=0; i < itemCount; i++){ //Create the items when the scene is created so that opening and closing the shop doesnt change whats in it
            usedSlots.Add(Random.Range(0,15));
        }
        usedSlots.Sort();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Open when the interact button is pressed (currently E)
    void OnInteract()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < detectRange || pauseMenu.activeInHierarchy){
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
    }
}
