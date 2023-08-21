using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputListener inputListener;
    private MoveComponent moveComponent;
    private ShootComponent shootComponent;
    private HealthComponent healthComponent;
    private UIComponent ui;

    private PhotonView photonView;

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveComponent = GetComponent<MoveComponent>();
        shootComponent = GetComponent<ShootComponent>();
        healthComponent = GetComponent<HealthComponent>();
        ui = GetComponent<UIComponent>();

        photonView = GetComponent<PhotonView>();

        healthComponent.TakeDamageEvent += ui.OnHealthChanged;
    }

    public void Init(PlayerInput input)
    {
        inputListener = new InputListener(input, photonView);
        moveComponent.SetInputListener(inputListener);
        shootComponent.SetInputListener(inputListener);
    }
}
