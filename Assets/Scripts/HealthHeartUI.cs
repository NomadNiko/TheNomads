using UnityEngine;
using UnityEngine.UI;

public class HealthHeartUI : MonoBehaviour {
    public Transform heartContainer;
    public GameObject heartPrefab;

    private Health playerHealth;
    private int maxHearts = 5;

    void Start() {
        // Find the player health component only if it exists
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            playerHealth = player.GetComponent<Health>();
        } else {
            Debug.LogWarning("Player not found. HealthHeartUI script disabled.");
            enabled = false; // Disable the script if the player is not found
        }

        // Initialize the hearts based on the player's initial health
        UpdateHearts();
    }

    void Update() {
        // Update the hearts when the player's health changes
        UpdateHearts();
    }

    void UpdateHearts() {
        // Check if the playerHealth reference is valid
        if (playerHealth == null) {
            Debug.LogWarning("PlayerHealth reference is null. HealthHeartUI script disabled.");
            enabled = false; // Disable the script if the player health reference is null
            return;
        }

        float healthPercentage = playerHealth.currentHealth / playerHealth.maxHealth;

        // Calculate the number of hearts to display based on the player's health percentage
        int displayedHearts = Mathf.CeilToInt(healthPercentage * maxHearts);

        // Ensure the heart container is assigned
        if (heartContainer == null) {
            Debug.LogWarning("HeartContainer is not assigned. HealthHeartUI script disabled.");
            enabled = false; // Disable the script if the heart container is not assigned
            return;
        }

        // Ensure the heart container has the correct number of hearts
        AdjustHeartContainer(displayedHearts);

        // Activate/deactivate hearts based on the player's health percentage
        for (int i = 0; i < heartContainer.childCount; i++) {
            if (i < displayedHearts) {
                heartContainer.GetChild(i).gameObject.SetActive(true);
            } else {
                heartContainer.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void AdjustHeartContainer(int numHearts) {
        // Ensure the heart prefab is assigned
        if (heartPrefab == null) {
            Debug.LogWarning("HeartPrefab is not assigned. HealthHeartUI script disabled.");
            enabled = false; // Disable the script if the heart prefab is not assigned
            return;
        }

        // Add or remove hearts from the container based on the required number
        while (heartContainer.childCount < numHearts) {
            Instantiate(heartPrefab, heartContainer);
        }

        while (heartContainer.childCount > numHearts) {
            Destroy(heartContainer.GetChild(heartContainer.childCount - 1).gameObject);
        }
    }
}
