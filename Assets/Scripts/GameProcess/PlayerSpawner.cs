using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private float minX, maxX, minY, maxY;

    [SerializeField] private PlayerInput playerInput;

    void Awake()
    {
        Vector2 randomPos = new(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity)
            .GetComponent<PlayerController>().Init(playerInput);
    }
}
