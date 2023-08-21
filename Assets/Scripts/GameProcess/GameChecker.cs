using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameChecker : MonoBehaviourPunCallbacks
{
    public List<PlayerController> Players { get; private set; }

    public event Action<string, int> WinEvent;

    void Start()
    {
        Players = FindObjectsOfType<PlayerController>().ToList();
        Debug.Log(Players.Count);
        foreach (PlayerController player in Players)
        {
            player.GetComponent<HealthComponent>().DeathEvent += CheckForAlive;
        }
    }

    void CheckForAlive()
    {
        PlayerController potentialWinner = null;
        
        int aliveCount = 0;
        foreach (var player in Players)
        {
            if (player.GetComponent<HealthComponent>().Health > 0)
            {
                aliveCount++;
                potentialWinner = player;
            }
                
        }

        if (aliveCount < 2)
        {
            Debug.Log("winscreeen");
            WinEvent(potentialWinner.GetComponent<PhotonView>().ViewID.ToString(),
                potentialWinner.GetComponent<ScoreComponent>().CoinAmount);
        }
    }
}
