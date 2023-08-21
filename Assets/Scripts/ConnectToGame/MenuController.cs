using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviourPunCallbacks
{
    private VisualElement root;

    private TextField createField;
    private TextField joinField;

    private Button createButton;
    private Button joinButton;

    [SerializeField] private int maxPlayers = 6;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        createField = root.Q<TextField>("createField");
        joinField = root.Q<TextField>("joinField");

        createButton = root.Q<Button>("createB");
        joinButton = root.Q<Button>("joinB");

        createButton.clicked += CreateRoom;
        joinButton.clicked += JoinRoom;
    }

    void CreateRoom()
    {
        if (createField.text == string.Empty) return;

        RoomOptions roomOptions = new();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createField.text, roomOptions);
    }

    void JoinRoom()
    {
        if (joinField.text == string.Empty) return;

        PhotonNetwork.LocalPlayer.NickName = Random.Range(0, 9999).ToString();
        PhotonNetwork.JoinRoom(joinField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined");
    }
}
