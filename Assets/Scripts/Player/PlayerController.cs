using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public string Nickname { get; private set; }

    private InputListener inputListener;

    private MoveComponent moveComponent;

    private ShootComponent shootComponent;

    private HealthComponent healthComponent;
    private HealthView healthView;

    private ScoreComponent scoreComponent;
    private ScoreView scoreView;

    private PhotonView photonView;

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveComponent = GetComponent<MoveComponent>();
        shootComponent = GetComponent<ShootComponent>();
        healthComponent = GetComponent<HealthComponent>();
        healthView = GetComponent<HealthView>();
        scoreComponent = GetComponent<ScoreComponent>();
        scoreView = GetComponent<ScoreView>();

        photonView = GetComponent<PhotonView>();

        healthComponent.TakeDamageEvent += healthView.OnHealthChanged;
        scoreComponent.MoneyChangedEvent += scoreView.OnMoneyChanged;
        
        if (!photonView.IsMine)
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    public void Init(string nick, PlayerInput input, UnityEngine.UIElements.VisualElement rootVE)
    {
        Nickname = nick;

        inputListener = new InputListener(input, photonView);
        moveComponent.SetInputListener(inputListener);
        shootComponent.SetInputListener(inputListener);
        scoreView.SetVisualElement(rootVE);
    }
}
