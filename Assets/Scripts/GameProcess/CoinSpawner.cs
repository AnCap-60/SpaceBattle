using Photon.Pun;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;

    [SerializeField] int coinAmount = 20;

    [SerializeField] float minX, maxX, minY, maxY;

    void Awake()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        for (int i = 0; i < coinAmount; i++)
        {
            Vector2 randomPos = new(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(coinPrefab.name, randomPos, Quaternion.identity);
        }
    }
}
