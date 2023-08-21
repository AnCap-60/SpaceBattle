using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    public int Damage { get; private set; } = 25;

    private Rigidbody2D rb;

    public int OwnerId { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Release(Vector2 direction, int ownerId)
    {
        Release(Damage, direction, speed, ownerId);
    }

    public void Release(int damage, Vector2 direction, float speed, int ownerId)
    {
        this.speed = speed;
        Damage = damage;
        rb.velocity = direction * speed;
        OwnerId = ownerId;
    }
}
