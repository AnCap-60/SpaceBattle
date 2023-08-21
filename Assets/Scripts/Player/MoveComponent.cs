using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private InputListener inputListener;
    private Rigidbody2D rb;
    [SerializeField] private GameObject rotatingObgject;


    [SerializeField] private float speed = 3f;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    public void SetInputListener(InputListener listener)
    {
        inputListener = listener;
        inputListener.MoveInputEvent += Move;
    }

    private void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        rb.velocity = direction * speed;
        //rb.AddForce(direction * speed);
        rotatingObgject.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) / Mathf.PI * 180 - 90);
    }

    private void FixedUpdate()
    {
        if (inputListener == null)
            return;

        //var dir = inputListener.playerInput.actions["Move"].ReadValue<Vector2>();
        //Move(dir * 5);
    }
}
