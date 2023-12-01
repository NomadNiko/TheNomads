using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour {
    [SerializeField] private BossController boss;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private PausedUI pausedUI;
    private GameObject player;
    private PlayerInput input;

    private bool isPaused = false;

    void Start() {
        // Find the player GameObject in the scene
        player = GameObject.FindWithTag("Player");

        // Start the Level Music
        SoundManager.Instance.PlaySound("FightMusic01", 90f, 100);
        input = new PlayerInput();
        input.CharacterControls.Pause.performed += ctx => PauseGame();
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
        Debug.Log("fdaskjhgf");
        if (!isPaused) {
            Time.timeScale = 0f; // Pause the game
            pausedUI.Show();
        }else{
            Time.timeScale = 1f; // Unpause the game (I assume this is the correct val)
            pausedUI.Hide();
        }
    }

    private void GameOver() {
        // Show GameOverUI and Pause Game
        gameOverUI.Show();
        PauseGame();
        
    }
}
