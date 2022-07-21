using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float slipperyFloat = .5f;
    [SerializeField] private AudioClip dashSFX;

    private float smoothedValue = 0.0f;
    private float velocity = 0.0F;

    private TrailRenderer trailRenderer;
    private float startMoveSpeed = 1f;
    private Vector2 movement;
    private float lastMoveX;
    private bool isDashing = false;
    private Coroutine currentDashCo = null;
    private VariableJoystick variableJoystick;

    private void Awake() {
        variableJoystick = FindObjectOfType<VariableJoystick>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        startMoveSpeed = moveSpeed;

    }


    private void Update()
    {
        PlayerInput();

        if ((Input.GetKeyDown(KeyCode.Space) || DetectSecondTouch()) && !isDashing) {
            StartCoroutine(DashCo(moveSpeed * 5, .3f));

            if (currentDashCo != null) {
                StopCoroutine(currentDashCo);
            }

            currentDashCo = StartCoroutine(TrailRendererToggleCo());
        }
    }

    private bool DetectSecondTouch() {
        if (Input.touchCount == 2)
            {
                Touch touch = Input.GetTouch(1);

                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }

        return false;
    }

    private void FixedUpdate() {
        Move();   
    }

    private IEnumerator DashCo(float v_start, float duration) {
        isDashing = true;

        AudioSource.PlayClipAtPoint(dashSFX, Camera.main.gameObject.transform.position, .1f);

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
        float moveX;
        float moveY;

        if (variableJoystick.Direction.x != 0) {
            moveX = variableJoystick.Direction.x;

            if (variableJoystick.Direction.x < 0) {
                lastMoveX = -1;
            } else {
                lastMoveX = 1;
            }
        } else {
            moveX = Input.GetAxisRaw("Horizontal");
        }

        if (variableJoystick.Direction.y != 0) {
            moveY = variableJoystick.Direction.y;
        } else {
            moveY = Input.GetAxisRaw("Vertical");
        }

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
