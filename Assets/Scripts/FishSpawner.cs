using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] float spawnFishTime = 2f;
    [SerializeField] GameObject[] fishPrefabs;
    [SerializeField] bool isLeftSide = true;
    [SerializeField] Transform fishParentObject;


    private void Start() {
        StartCoroutine(SpawnFishCo());
    }

    private IEnumerator SpawnFishCo() {
        while (true)
        {
            int randomFishNum = Random.Range(0, fishPrefabs.Length);
            Collider2D thisCollider = GetComponent<BoxCollider2D>();
            GameObject newFish = Instantiate(fishPrefabs[randomFishNum], RandomPointInBounds(thisCollider.bounds), transform.rotation);

            if (!isLeftSide) {
                newFish.GetComponent<FishMovement>().moveSpeed *= -1;
                newFish.GetComponent<FishMovement>().spriteRenderer.flipX = true;
            }

            newFish.transform.parent = fishParentObject;
            yield return new WaitForSeconds(spawnFishTime);
        }
    }

    public Vector2 RandomPointInBounds(Bounds bounds) {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
