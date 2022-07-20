using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Fish>()) {
            Destroy(other.gameObject);
        }
    }
}
