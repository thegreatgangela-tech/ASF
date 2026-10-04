using UnityEngine;

public class Bullet : MonoBehaviour //Script written by Joel and YiFan
{
    public float speed = 50f;
    public float lifeTime = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
{
    npcRunning npc = other.GetComponentInParent<npcRunning>();
    if (npc != null)
    {
        npc.Damage(20f); // ALWAYS 20
    }
    Destroy(gameObject);
}


}
