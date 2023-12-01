// 2023-12-01 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour {
    // Static instance of PausedUI
    public static PausedUI Instance { get; private set; }

    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;

    // Awake method to handle instance creation
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    private void Start() {
        retryButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    // Method to restart the level
    public void RestartGame() {
        SceneManager.LoadScene("GameScene"); //This wasn't working in thje gameover scene.
    }

    // Method to exit to main menu
    public void ExitGame() {
        // Terminate the application
        SceneManager.LoadScene("MainMenu");
    }
}
