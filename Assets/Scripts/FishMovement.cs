using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] float changeDirectionTime = 2f;
    [SerializeField] float xMoveValue = 1f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public SpriteRenderer spriteRenderer;



    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(ChangeDirection());

        moveSpeed += Random.Range(-0.5f, .5f);
    }   

    private void FixedUpdate()
    {
        Move();
    }


    IEnumerator ChangeDirection()
    {
        while (true)
        {
            float RandomY = Random.Range(-0.5f, 0.5f);

            moveDirection = new Vector2(Mathf.Abs(moveSpeed), RandomY).normalized;
            ChangeDirectionTime();
            yield return new WaitForSeconds(changeDirectionTime);
        }
    }

    private void ChangeDirectionTime() {
        changeDirectionTime = Random.Range(1f, 3f);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
