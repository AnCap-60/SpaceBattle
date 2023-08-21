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
                health = 0;
                DeathEvent();
            }
        }
    }

    public event Action<int> TakeDamageEvent;

    public event Action DeathEvent;

    PhotonView photonView;

    void Awake()
    {
        TakeDamageEvent += TakeDamage;
        DeathEvent += OnDeath;

        photonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //if (!photonView.IsMine) return;

        if (collider != null && collider.CompareTag("Projectile"))
        {
            if (!collider.TryGetComponent<ProjectileController>(out var projectile)) return;

            if (projectile.OwnerId == photonView.ViewID) return;

            if (photonView.IsMine)
                photonView.RPC("TakeDamageRPC", RpcTarget.All, projectile.Damage);

            if (projectile.GetComponent<PhotonView>().IsMine && photonView.IsMine)
                PhotonNetwork.Destroy(projectile.gameObject);
        }
    }

    void OnDeath()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void TakeDamageRPC(int damage)
    {
        TakeDamageEvent(damage);
    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
