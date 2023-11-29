using UnityEngine;
using UnityEngine.UI;

public class HealthHeartUI : MonoBehaviour {
    public GameObject healthHeartImagePrefab;
    public Transform container;
    public Health playersHealth;

    private int maxHearts = 5;

    private void Update() {
        UpdateHearts();
    }

    private void UpdateHearts() {
        // Clear existing hearts
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }

        float currentHealth = playersHealth.GetHealth();
        int heartsToShow = Mathf.Clamp(Mathf.FloorToInt((currentHealth) / 20), 0, maxHearts);

        for (int i = 0; i < heartsToShow; i++) {
            Instantiate(healthHeartImagePrefab, container);
        }
    }

    // Attach this method to an event in your PlayersHealth script
    public void OnHealthChanged() {
        UpdateHearts();
    }
}
