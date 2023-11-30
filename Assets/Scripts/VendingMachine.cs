using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// A lot of this code is unused, if this project continues into the future it will get overhauled but for right now I am rushing
public class VendingMachine : MonoBehaviour {
    // Start is called before the first frame update
    public int itemCount = 4;
    public List<int> usedSlots = new List<int>();

    private float detectRange = 10.0f;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player; //Will need this to check proximity so only opens when we mean it to

    [SerializeField] private GameObject buyPrompt;

    //All the Prefabs we can buy
    [SerializeField] private GameObject speedPowerup;
    [SerializeField] private GameObject sizePowerup;
    [SerializeField] private GameObject molotov;
    [SerializeField] private GameObject projectile;

    void Start() {
        // for (int i = 0; i < itemCount; i++) {
        //     // Create the items when the scene is created so that opening and closing the shop doesn't change what's in it
        //     usedSlots.Add(Random.Range(0, 15));
        // }
        // usedSlots.Sort();
    }

    // Update is called once per frame
    void Update() {
        // You can add any update logic here if needed
        if (Vector3.Distance(player.transform.position, transform.position) < detectRange){
            buyPrompt.SetActive(true);
        } else if (buyPrompt.activeInHierarchy && Vector3.Distance(player.transform.position, transform.position) > detectRange) {
            buyPrompt.SetActive(false);
        }
    }

    // Open when the interact button is pressed (currently E)
    public void OnInteract() {
        if (player.GetComponent<Scales>().scales >= 5 && Vector3.Distance(player.transform.position, transform.position) < detectRange){
            player.GetComponent<Scales>().AddScales(-5);
            int itemBought = Random.Range(0,5);
            Vector3 spawnLoc = new Vector3(Random.Range(-20.0f, 20.0f), 1.5f, Random.Range(-20.0f, 20.0f)); //change depending on level layout
            if (itemBought == 0){
                player.GetComponent<Health>().Heal(20); //Heal for 20
            } else if (itemBought == 1){ //Create different powerups
                Instantiate(speedPowerup, spawnLoc, Quaternion.identity);
            } else if (itemBought == 2){
                Instantiate(sizePowerup, spawnLoc, Quaternion.identity);
            } else if (itemBought == 3){
                Instantiate(molotov, spawnLoc, Quaternion.identity);
            } else if (itemBought == 4){
                Instantiate(projectile, spawnLoc, Quaternion.identity);
            }
        }
    }

    // Method to handle healing based on the selected item
    private void HandleHealing() {
        int selectedItemRow = GetComponent<ShopController>().selected[0];
        int selectedItemColumn = GetComponent<ShopController>().selected[1];

        // Assuming that 10% of total health is the healing value for A1
        float percentageHeal = 0.1f;

        switch (selectedItemRow) {
            case 0:
                switch (selectedItemColumn) {
                    case 0: // A1
                        // Heal by 10% of total health, up to 100%
                        float healAmount = player.GetComponent<Health>().GetMaxHealth() * percentageHeal;
                        player.GetComponent<Health>().Heal(Mathf.Min(healAmount, player.GetComponent<Health>().GetMaxHealth()));
                        break;

                    case 1: // A2
                        // Heal to 100% of total health
                        player.GetComponent<Health>().Heal(player.GetComponent<Health>().GetMaxHealth());
                        break;

                        // Handle other columns if needed
                }
                break;

                // Handle other rows if needed
        }
    }
}
