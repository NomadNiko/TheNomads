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
        if (player == null) { //We were destroying the player and then trying to check its health. This is just the quickest work around
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
