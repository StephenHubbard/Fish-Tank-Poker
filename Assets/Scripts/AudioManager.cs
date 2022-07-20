using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] fishCrunchSFX;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void EatFishCrunch() {
        audioSource.clip = fishCrunchSFX[Random.Range(0, fishCrunchSFX.Length)];
        audioSource.Play();
    }
}
