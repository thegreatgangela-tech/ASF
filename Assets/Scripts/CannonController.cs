using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public Terrain terrain;
    private float heightOffset = 0.5f; // will be adjusted dynamically

    [Header("Cannon Parts")]
    public Transform cannonBody;
    public Transform cannonBarrel;
    public Transform bulletSpawnPoint;

    [Header("Aiming Settings")]
    public float sensitivity = 0.2f;
    public float minPitch = -20f;
    public float maxPitch = 60f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float shootForce = 50f;
    public float shootRate = 0.3f;

    private float yaw;
    private float pitch;
    private float nextShootTime = 0f;

    void Start()
    {
        // Lock cursor for aiming
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize terrain
        if (terrain == null) terrain = Terrain.activeTerrain;

        // Adjust heightOffset based on collider
        Collider col = GetComponent<Collider>();
        if (col != null)
            heightOffset = col.bounds.extents.y; // bottom of cannon touches terrain

        // Make Rigidbody kinematic
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // Initialize rotation values
        yaw = cannonBody.localEulerAngles.y;
        pitch = cannonBarrel.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        // Place cannon on terrain at start
        Vector3 startPos = transform.position;
        float terrainHeight = terrain.SampleHeight(startPos) + terrain.transform.position.y;
        startPos.y = terrainHeight + heightOffset;
        transform.position = startPos;
    }

    void Update()
    {
        HandleMovement();
        HandleAiming();
        HandleShooting();
    }

    void HandleMovement()
    {
        if (Keyboard.current == null || terrain == null) return;

        // Flatten cannon axes for horizontal movement
        Vector3 forward = transform.forward; forward.y = 0f; forward.Normalize();
        Vector3 right = transform.right; right.y = 0f; right.Normalize();

        Vector3 move = Vector3.zero;
        if (Keyboard.current.wKey.isPressed) move += forward;
        if (Keyboard.current.sKey.isPressed) move -= forward;
        if (Keyboard.current.aKey.isPressed) move -= right;
        if (Keyboard.current.dKey.isPressed) move += right;

        if (move.sqrMagnitude > 0.001f)
        {
            // Movement step
            move = move.normalized * moveSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + move;

            // Clamp to terrain height
            float terrainHeight = terrain.SampleHeight(newPos) + terrain.transform.position.y;
            newPos.y = terrainHeight + heightOffset;

            transform.position = newPos;

            // Smooth rotation toward movement
            Vector3 lookDir = move;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void HandleAiming()
    {
        if (Mouse.current == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue();
        yaw += delta.x * sensitivity;
        pitch -= delta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cannonBody.localRotation = Quaternion.Euler(0f, yaw, 0f);
        cannonBarrel.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void HandleShooting()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= nextShootTime)
        {
            ShootBullet();
            nextShootTime = Time.time + shootRate;
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null) return;

        GameObject b = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.AddForce(bulletSpawnPoint.forward * shootForce, ForceMode.Impulse);
        }

        Destroy(b, 5f);
    }
}