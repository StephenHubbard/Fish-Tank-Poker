using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float slipperyFloat = .5f;

    private float smoothedValue = 0.0f;
    private float velocity = 0.0F;

    private TrailRenderer trailRenderer;
    private float startMoveSpeed = 1f;
    private Vector2 movement;
    private float lastMoveX;
    private bool isDashing = false;
    private Coroutine currentDashCo = null;

    private void Awake() {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        startMoveSpeed = moveSpeed;

    }


    private void Update()
    {
        PlayerInput();

        if (Input.GetMouseButtonDown(0) && !isDashing) {
            StartCoroutine(DashCo(moveSpeed * 5, .3f));

            if (currentDashCo != null) {
                StopCoroutine(currentDashCo);
            }
            
            currentDashCo = StartCoroutine(TrailRendererToggleCo());
        }
    }

    private void FixedUpdate() {
        Move();   
    }

    private IEnumerator DashCo(float v_start, float duration) {
        isDashing = true;

        float elapsed = 0.0f;
        while (elapsed < duration )
        {
            moveSpeed = Mathf.Lerp(v_start, startMoveSpeed, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }

        moveSpeed = startMoveSpeed;
        isDashing = false;
    }

    private IEnumerator TrailRendererToggleCo() {
        trailRenderer.enabled = true;
        yield return new WaitForSeconds(1f);
        trailRenderer.enabled = false;
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
