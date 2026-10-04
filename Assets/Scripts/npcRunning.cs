using UnityEngine;
//Code written by Joel and Wang YiFan
public class npcRunning : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    [Header("Health")]
    public float NPC_health = 100f;
    private bool isDead = false;

    [Header("Terrain Settings")]
    public Terrain terrain;

    [Header("Stopping Distance")]
    public float stopDistance = 0.5f;

    void Update() //Assisted by AI to assist with quaternion and look towards base
    {
        if (isDead || target == null || terrain == null) return;

        // Direction toward target (XZ only)
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        // Rotate to face target
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        // Move toward target
        if (dir.magnitude > stopDistance)
        {
            Vector3 nextPos = transform.position + dir.normalized * speed * Time.deltaTime;
            nextPos.y = terrain.SampleHeight(nextPos);
            transform.position = nextPos;
        }
    }

    
    public void Damage(float damageAmount)
    {
        if (isDead) return;

        NPC_health -= damageAmount;

        if (NPC_health <= 0f)
        {
            Die();
        }
    }

    //Assisted by AI
    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("House"))
        {
            GameManager.Instance.DamageBase(10); // Reduce house health
            Die(); // NPC dies on contact
        }
    }

//Assited by AI
    private void Die()
    {
        isDead = true;

        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(100);

        Destroy(gameObject);
    }
}