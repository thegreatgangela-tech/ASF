using UnityEngine;
using UnityEngine.InputSystem;

public class CannonAim : MonoBehaviour
{
    [Header("Cannon Parts")]
    public Transform cannonBody;   // Cannon root (parent of Aim + wheels)
    public Transform cannonBarrel; // The "Aim" mesh
    public float sensitivity = 0.2f;

    [Header("Pitch Limits")]
    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw;   // horizontal rotation
    private float pitch; // vertical rotation

    void Start()
    {
        // lock cursor for aiming
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = cannonBody.eulerAngles.y;
        pitch = cannonBarrel.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f; // convert to -180..180
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // Read mouse delta
        Vector2 delta = Mouse.current.delta.ReadValue();

        yaw += delta.x * sensitivity;
        pitch -= delta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Apply rotations
        cannonBody.localRotation = Quaternion.Euler(0f, yaw, 0f);
        cannonBarrel.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
