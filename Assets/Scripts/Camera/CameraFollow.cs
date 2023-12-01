using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;       // Reference to the player GameObject
    public float smoothing = 5f;    // Smoothing factor for camera movement

    private Vector3 offset;         // Offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;
        // Create a postion for the camera to move towards
        Vector3 targetCamPos = player.transform.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}