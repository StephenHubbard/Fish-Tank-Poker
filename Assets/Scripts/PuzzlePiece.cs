using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private AudioClip puzzlePiece;

    private TimeLeft timeLeft;
    private Rigidbody2D rb;
    private bool rotateLeft = true;

    private void Awake() {
        timeLeft = FindObjectOfType<TimeLeft>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        DetermineRotationDir();
    }

    private void DetermineRotationDir()
    {
        if (Random.value >= 0.5)
        {
        rotateLeft = true;
        }
        rotateLeft = false;
    }

    private void Update() {
        if (rotateLeft) {
            rb.rotation += 20f * Time.deltaTime;
        } else {
            rb.rotation += -20f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            timeLeft.PuzzlePieceAddTime();
            AudioSource.PlayClipAtPoint(puzzlePiece, Camera.main.gameObject.transform.position, .7f);
            Destroy(gameObject);
        }
    }
}
