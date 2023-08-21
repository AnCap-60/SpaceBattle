using Photon.Pun;
using System;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float projectileSpeed = 3;
    [SerializeField] int projectileDamage = 1;

    [SerializeField] Transform modelTransform;

    [SerializeField] double cooldown = 0.4;
    double lastShootTime = 0;

    public event Action ShootEvent;

    Vector2 direction = Vector2.up;

    public void SetInputListener(InputListener listener)
    {
        listener.ShootInputEvent += TryShoot;
        listener.MoveInputEvent += UpdateShootingDirection;

        ShootEvent += Shoot;
    }

    void UpdateShootingDirection(Vector2 dir)
    {
        direction = dir;
    }

    void TryShoot()
    {
        if ((Time.timeSinceLevelLoadAsDouble - lastShootTime) >= cooldown)
        {
            ShootEvent();
        }
    }

    void Shoot()
    {
        var projectile = PhotonNetwork.Instantiate(projectilePrefab.name, transform.position,
            transform.rotation).GetComponent<ProjectileController>();
        projectile.Release(projectileDamage, direction.normalized * 5, projectileSpeed, GetComponent<PhotonView>().ViewID);

        lastShootTime = Time.timeSinceLevelLoadAsDouble;
    }
}
