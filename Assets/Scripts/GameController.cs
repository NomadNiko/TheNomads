using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    [SerializeField] private BossController boss;
    [SerializeField] private GameOverUI gameOverUI;
    private GameObject player;

    void Start() {
        // Find the player GameObject in the scene
        player = GameObject.FindWithTag("Player");
    }

    void Update() {
        PlayerDeathCheck();
    }

    private void PlayerDeathCheck() {
        // Trigger game over if players Health reaches 0
        if (player != null && player.GetComponent<Health>() != null && player.GetComponent<Health>().currentHealth <= 0) {
            GameOver();
        }
    }
    
    public void PauseGame() {
        Time.timeScale = 0f; // Pause the game
    }

    private void GameOver() {
        // Show GameOverUI and Pause Game
        gameOverUI.Show();
        PauseGame();
        
    }
}
