using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent( typeof(PlayerInput) )]
[RequireComponent( typeof(Rigidbody2D) )]
[RequireComponent( typeof(Animator) )]
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float _moveSpeed = 5f;

    [Header("Jump")]
    public float _jumpPower = 10f;
    public float _gravityScale = 7f;

    [Header("Ground Check")]
    public LayerMask _groundLayer;
    public Collider2D _groundCollider;

    Rigidbody2D _rb;

    Animator _animator;

    PlayerInput _playerInput;
    InputAction _jumpAction;
    InputAction _moveAction;
    float _horizontalMovement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = _gravityScale;

        _playerInput = GetComponent<PlayerInput>();
        _jumpAction = _playerInput.actions["Jump"];
        _moveAction = _playerInput.actions["Move"];

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        UpdateFlip();
        UpdateSprite();
    }

    void UpdateSprite()
    {
        UpdateFlip();

        if (_rb.velocity.y > 0.005)
        {
            _animator.Play("Shuuto Jump");
        }
        else if (_rb.velocity.y < -0.005)
        {
            _animator.Play("Shuuto Fall");
        }
        else if (Math.Abs(_horizontalMovement) > 0.005)
        {
            _animator.Play("Shuuto Walk");
        }
        else
        {
            _animator.Play("Shuuto Idle");
        }
    }


    void HandleInput()
    {
        //read move input
        _horizontalMovement = _moveAction.ReadValue<Vector2>().x * _moveSpeed;
        
        //jump check
        if (_jumpAction.WasPerformedThisFrame() && isOnGround())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);

        }
        else if (_jumpAction.WasReleasedThisFrame())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    void UpdateFlip()
    {
        if (_horizontalMovement > 0.005 && transform.localScale.x < 0 || _horizontalMovement < -0.005 && transform.localScale.x > 0)
        {
            //flip the horizontal scale
            var scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }

    void FixedUpdate() {
        
        _rb.velocity = new Vector2( _horizontalMovement, _rb.velocity.y);
    }

    public bool isOnGround()
    {
        bool doesOverlap = Physics2D.OverlapBox(
            _groundCollider.bounds.center,
            _groundCollider.bounds.size,
            0,
            _groundLayer
            );
        return doesOverlap;

    }


    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamage(float amount)
    {
        ResetScene();
    }    
}
