using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
    private static GameOverUI instance;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private void Start() {
        if (instance == null) {
            instance = this;
            Hide();
        } else {
            Debug.LogWarning("Multiple instances of GameOverUI found. Only one instance is allowed.");
            Destroy(gameObject);
        }

        // Attach functions to button clicks
        restartButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public static GameOverUI Instance {
        get {
            return instance;
        }
    }

    public void Show() {
        if (instance != null) {
            instance.gameObject.SetActive(true);
        } else {
            Debug.LogError("GameOverUI instance is null. Make sure the script is attached to a GameObject in the scene.");
        }
    }

    public void Hide() {
        if (instance != null) {
            instance.gameObject.SetActive(false);
        } else {
            Debug.LogError("GameOverUI instance is null. Make sure the script is attached to a GameObject in the scene.");
        }
    }

    
    private void StartGame() {
        // Load the GameScene
        SceneManager.LoadScene("ShrimpFight");
    }

    private void ExitGame() {
        // Exit the game
        SceneManager.LoadScene("MainMenu");
    }
}
