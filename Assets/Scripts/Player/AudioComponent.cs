using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    [SerializeField] List<AudioClip> clipList;

    [SerializeField] AudioSource source;

    private void Awake()
    {
        ScoreComponent score = GetComponent<ScoreComponent>();
        score.MoneyChangedEvent += money => PlaySound("earnCoinSound");

        ShootComponent shoot = GetComponent<ShootComponent>();
        shoot.ShootEvent += () => PlaySound("shootSound");

        HealthComponent health = GetComponent<HealthComponent>();
        health.TakeDamageEvent += damage => PlaySound("hurtSound");
    }

    void PlaySound(string clipName)
    {
        source.PlayOneShot(clipList.Find(c => c.name == clipName));
    }
}
