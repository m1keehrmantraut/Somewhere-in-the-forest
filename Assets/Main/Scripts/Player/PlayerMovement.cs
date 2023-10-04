using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Joystick joystick;

    [SerializeField] private Animator _animator;

    [SerializeField] private AttackMode attackButton;
    // private AudioSource _audioSource;
    // [SerializeField] private AudioClip walkingSound;

    [SerializeField] private ParticleSystem dustParticles;

    private Folowing sword;
    
    Vector2 moveInput;
    Vector2 moveVelocity;

    private void Start()
    {
        sword = GameObject.FindWithTag("Sword").GetComponent<Folowing>();
    }

    private void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * moveSpeed;

        _animator.SetFloat("Horizontal", moveInput.x);
        _animator.SetFloat("Vertical", moveInput.y);
        _animator.SetFloat("Speed", moveVelocity.magnitude);

        if (moveInput.x > 0)
        {
            sword.ratio = -1;
        }
        else if (moveInput.x < 0)
        {
            sword.ratio = 1;
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         attackButton.ChangeAttackMode("Melee");
    //     }
    //     else
    //     {
    //         attackButton.ChangeAttackMode("Shooting");
    //     }
    // }
}

