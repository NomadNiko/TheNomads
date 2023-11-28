using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour {
    // Start is called before the first frame update
    public int itemCount = 4;
    public List<int> usedSlots = new List<int>();

    private float detectRange = 10.0f;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player; //Will need this to check proximity so only opens when we mean it to

    void Start() {
        for (int i = 0; i < itemCount; i++) {
            // Create the items when the scene is created so that opening and closing the shop doesn't change what's in it
            usedSlots.Add(Random.Range(0, 15));
        }
        usedSlots.Sort();
    }

    // Update is called once per frame
    void Update() {
        // You can add any update logic here if needed
    }

    // Open when the interact button is pressed (currently E)
    public void OnInteract() {
        if (Vector3.Distance(player.transform.position, transform.position) < detectRange || pauseMenu.activeInHierarchy) {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);

            // Check if the shop is open
            if (pauseMenu.activeInHierarchy) {
                // Handle healing based on the selected item
                HandleHealing();
            }
        }
    }

    // Method to handle healing based on the selected item
    private void HandleHealing() {
        int selectedItemRow = GetComponent<shopController>().selected[0];
        int selectedItemColumn = GetComponent<shopController>().selected[1];

        // Assuming that 10% of total health is the healing value for A1
        float percentageHeal = 0.1f;

        switch (selectedItemRow) {
            case 0:
                switch (selectedItemColumn) {
                    case 0: // A1
                        // Heal by 10% of total health, up to 100%
                        float healAmount = player.GetComponent<Health>().maxHealth * percentageHeal;
                        player.GetComponent<Health>().Heal(Mathf.Min(healAmount, player.GetComponent<Health>().maxHealth));
                        break;

                    case 1: // A2
                        // Heal to 100% of total health
                        player.GetComponent<Health>().Heal(player.GetComponent<Health>().maxHealth);
                        break;

                        // Handle other columns if needed
                }
                break;

                // Handle other rows if needed
        }
    }
}
