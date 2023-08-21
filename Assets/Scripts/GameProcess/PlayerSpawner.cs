using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    [SerializeField] float minX, maxX, minY, maxY;

    [SerializeField] PlayerInput playerInput;

    [SerializeField] UIDocument ui;

    void Awake()
    {
        Vector2 randomPos = new(Random.Range(minX, maxX), Random.Range(minY, maxY));
        var player = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity).GetComponent<PlayerController>();
        player.Init(PhotonNetwork.LocalPlayer.NickName, playerInput, ui.rootVisualElement);
    }
}
