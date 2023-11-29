using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    [SerializeField] private BossController boss;
    [SerializeField] private GameOverUI gameOverUI;

    void Update() {
        // Find the player GameObject in the scene
        GameObject player = GameObject.FindWithTag("Player");

        // Check player health and trigger game over if it's zero
        if (player != null && player.GetComponent<Health>() != null && player.GetComponent<Health>().currentHealth <= 0) {
            GameOver();
        }

        // Check boss health and destroy if it's zero
        if (boss != null && boss.GetComponent<Health>() != null && boss.GetComponent<Health>().currentHealth <= 0) {
            DestroyBoss();
        }
    }

    private void GameOver() {
        gameOverUI.Show();
        Time.timeScale = 0f; // Stop the game
    }

    private void DestroyBoss() {
        Destroy(boss.gameObject);
    }
}
