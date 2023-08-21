using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener
{
    public event Action<Vector2> MoveInputEvent;
    public event Action ShootInputEvent;

    public PlayerInput playerInput;

    private Rigidbody2D rb;

    PhotonView view;

    public InputListener(PlayerInput input, PhotonView view)
    {
        playerInput = input;
        input.onActionTriggered += ActionTriggered;
        this.view = view;
    }

    private void ActionTriggered(InputAction.CallbackContext context)
    {
        if (!view.IsMine && !context.action.triggered)
            return;

        switch (context.action.name)
        {
            case "Move":
                if (!context.action.inProgress) return;

                Vector2 direction = context.action.ReadValue<Vector2>();
                MoveInputEvent(direction);
                break;

            case "Fire":
                ShootInputEvent();
                break;

            default:
                break;
        }
    }
}
