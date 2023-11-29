using UnityEngine;

public class GameOverUI : MonoBehaviour {
    private static GameOverUI instance;

    private void Start() {
        if (instance == null) {
            instance = this;
            Hide();
        } else {
            Debug.LogWarning("Multiple instances of GameOverUI found. Only one instance is allowed.");
            Destroy(gameObject);
        }
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
}
