using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Photon.Realtime;

public class GameChecker : MonoBehaviourPunCallbacks
{
    public List<PlayerController> Players { get; private set; }

    public event Action<string, int> WinEvent;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        StartCoroutine(UpdatePlayersList());
    }

    void Start()
    {
        StartCoroutine(UpdatePlayersList());
    }

    IEnumerator UpdatePlayersList()
    {
        yield return new WaitForSeconds(1f);

        Players = FindObjectsOfType<PlayerController>().ToList();

        foreach (PlayerController player in Players)
        {
            player.GetComponent<HealthComponent>().DeathEvent += CheckForAlive;
        }
        Debug.Log(Players.Count);
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
                Debug.Log("alive: " + potentialWinner);
            }
                
        }

        if (aliveCount == 1)
        {
            Debug.Log("winscreeen");
            Debug.Log(potentialWinner);
            GetComponent<PhotonView>().RPC("WinRPC", RpcTarget.All,
                potentialWinner.GetComponent<PhotonView>().ViewID.ToString(),
                potentialWinner.GetComponent<ScoreComponent>().CoinAmount);
        }
    }

    [PunRPC]
    void WinRPC(string nick, int coins)
    {
        WinEvent(nick, coins);
    }
}
