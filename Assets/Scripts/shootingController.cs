using UnityEngine;
using UnityEngine.InputSystem;

public class shootingController : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float shootRate;
    public float shootForce = 500f;
        void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Left mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }


    void Shoot()
    {
        GameObject spawnedBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawnPoint.forward * shootForce, ForceMode.Impulse);
    }
}
