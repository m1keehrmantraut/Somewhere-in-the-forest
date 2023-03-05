using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 6f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Joystick joystick;

    Vector2 moveInput;
    Vector2 moveVelocity;

     private void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}

