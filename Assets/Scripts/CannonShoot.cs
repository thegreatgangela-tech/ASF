using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CannonShoot : MonoBehaviour //Code written by Joel and YiFan 
{
    [Header("Fire Settings")]
    public Transform firePoint;
    public float range = 200f;
    public float damage = 20f;
    public float radius = 0.5f;      // SphereCast radius
    public float fireRate = 0.5f;    // Seconds between shots

    [Header("Input")]
    public InputActionAsset inputActions;

    private InputAction shootAction;
    private bool isFiring = false;

    private void OnEnable()
    {
        shootAction = inputActions.FindAction("Gameplay/Shoot");

        if (shootAction != null)
        {
            shootAction.performed += ctx => StartFiring();
            shootAction.canceled += ctx => StopFiring();
        }

        shootAction?.Enable();
    }

    private void OnDisable()
    {
        shootAction?.Disable();
    }

    private void StartFiring()
    {
        if (!isFiring)
            StartCoroutine(FireContinuously());
    }

    private void StopFiring()
    {
        isFiring = false;
    }

    private IEnumerator FireContinuously()
    {
        isFiring = true;

        while (isFiring)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate); //code written by AI below
        }
    }

    private void Shoot() //entire method written by AI
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, radius, out hit, range))
        {
            npcRunning npc = hit.collider.GetComponentInParent<npcRunning>();
            if (npc != null)
            {
                npc.Damage(damage);
                Debug.Log("HIT NPC at distance: " + hit.distance);
            }
        }

        Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 0.5f);
    }
}
