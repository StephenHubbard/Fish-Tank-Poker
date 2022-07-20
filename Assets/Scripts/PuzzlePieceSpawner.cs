using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceSpawner : MonoBehaviour
{
    [SerializeField] float spawnPuzzleTimer = 2f;
    [SerializeField] GameObject puzzlePiecePrefab;


    private void Start() {
        StartCoroutine(SpawnPuzzlePiece());
    }

    private IEnumerator SpawnPuzzlePiece() {
        while (true)
        {
            Collider2D thisCollider = GetComponent<BoxCollider2D>();
            GameObject newPuzzlePiece = Instantiate(puzzlePiecePrefab, RandomPointInBounds(thisCollider.bounds), transform.rotation);

            yield return new WaitForSeconds(spawnPuzzleTimer);
        }
    }

    public Vector2 RandomPointInBounds(Bounds bounds) {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
