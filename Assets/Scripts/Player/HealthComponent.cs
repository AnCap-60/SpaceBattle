using Photon.Pun;
using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private int health = 100;
    public int Health
    {
        get => health;
        set
        {
            health = value;

            if (health <= 0)
            {
                Destroy(gameObject);
                PhotonNetwork.LoadLevel("Lobby");
            }
        }
    }

    public event Action<int> TakeDamageEvent;

    private void Awake()
    {
        TakeDamageEvent += TakeDamage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null && collider.CompareTag("Projectile"))
        {
            var projectile = collider.GetComponent<ProjectileController>();

            if (projectile.OwnerId == gameObject.GetHashCode())
                return;

            TakeDamageEvent(projectile.Damage);
            Destroy(projectile);
        }
    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
