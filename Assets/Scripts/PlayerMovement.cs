using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Vector2 movement;
    private float lastMoveX;

    private void Start()
    {
        
    }


    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate() {
        Move();   
    }

    
    private void PlayerInput() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.A)) {
            lastMoveX = -1;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            lastMoveX = 1;
        }

        if (lastMoveX <= -1) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
