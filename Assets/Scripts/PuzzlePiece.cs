using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private TimeLeft timeLeft;
    private Rigidbody2D rb;
    private bool rotateLeft = true;
    private AudioManager audioManager;

    private void Awake() {
        timeLeft = FindObjectOfType<TimeLeft>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
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
            audioManager.PickUpPuzzlePiece();
            Destroy(gameObject);
        }
    }
}
