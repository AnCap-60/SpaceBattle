using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WinScreen : MonoBehaviour
{
    VisualElement root;

    [SerializeField] GameChecker gameChecker;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        gameChecker.WinEvent += ShowWinScreen;
    }

    void ShowWinScreen(string nick, int coins)
    {
        Label nickL = root.Q<Label>("winnerNick");
        Label coinsL = root.Q<Label>("winnerCoins");

        nickL.text = nick;
        coinsL.text = coins.ToString();

        root.Q<VisualElement>("winWindow").style.display = DisplayStyle.Flex;

        StartCoroutine(ReturnToLobby());
    }

    IEnumerator ReturnToLobby()
    {
        yield return new WaitForSecondsRealtime(4f);

        PhotonNetwork.LoadLevel("Game");
    }
}
