using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start() {
        // Start Themesong
        SoundManager.Instance.PlaySound("TheNomadsTheme01", 70f, 1000);

        // Attach functions to button clicks
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame() {
        // Load the GameScene
        SceneManager.LoadScene("ShrimpFight");
    }

    private void ExitGame() {
        // Exit the game
        Application.Quit();
    }
}
