using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum GameState {
    WaitingToStart,
    Playing,
    Paused,
    GameOver
}

public class GameController : MonoBehaviour {
    //[SerializeField] private BossController boss;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private PausedUI pausedUI;
    [SerializeField] private GameObject pressSpaceUI;
    [SerializeField] private PlayerInput input;

    private GameObject player;
    
    private GameState gameState = GameState.WaitingToStart;

    private void Awake() {
        input = new PlayerInput();

        // Check if the CharacterControls is not null before subscribing to events
        if (input != null) {
            input.CharacterControls.Pause.performed += ctx => HandlePauseInput();
            input.CharacterControls.Attack_A.performed += ctx => HandleFirstInput();
        }
    }

    void Start() {
        // Find the player GameObject in the scene
        player = GameObject.FindWithTag("Player");

        // Start the Level Music
        SoundManager.Instance.PlaySound("FightMusic01", 70f, 100);


        // Initialize the game state
        SetGameState(GameState.WaitingToStart);
    }





    void Update() {
        if (gameState == GameState.Playing) {
            // Update game-related logic for the Playing state
            PlayerDeathCheck();
        }
    }

    private void PlayerDeathCheck() {
        // Trigger game over if players Health reaches 0
        if (player == null) {
            SetGameState(GameState.GameOver);
        }
    }

    private void HandleFirstInput() {
        if (gameState == GameState.WaitingToStart) {
            // Transition to Playing state when the Attack key is pressed
            SetGameState(GameState.Playing);
        }
    }

    private void HandlePauseInput() {
        if (gameState == GameState.Playing) {
            // Transition to Paused state when the Pause key is pressed
            SetGameState(GameState.Paused);
        } else if (gameState == GameState.Paused) {
            // Transition back to Playing state when the Pause key is pressed again
            SetGameState(GameState.Playing);
        }
    }

    private void SetGameState(GameState newState) {
        gameState = newState;

        switch (newState) {
            case GameState.WaitingToStart:
                Time.timeScale = 0f;
                pressSpaceUI.SetActive(true);
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                pressSpaceUI.SetActive(false);
                pausedUI.Hide();
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                pausedUI.Show();
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                gameOverUI.Show();
                break;
        }
    }
    private void OnEnable() {
        // Enable character controls on script enable
        input.CharacterControls.Enable(); 
    }

    private void OnDisable() {
        // Disable character controls on script disable
        input.CharacterControls.Disable();
    }
}
