using UnityEngine;

public class PressSpaceUI : MonoBehaviour
{
    public static PressSpaceUI Instance { get; private set; }

    private void Start() {
        Hide();
    }


    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}
