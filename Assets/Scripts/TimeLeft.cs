using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{
    [SerializeField] private float startingTime = 30f;
    [SerializeField] private Slider slider;
    [SerializeField] private float timeStillLeft;
    [SerializeField] private float timeToAdd = 10f;

    private void Start() {
        timeStillLeft = startingTime;

        slider.maxValue = startingTime;
        slider.value = startingTime;
    }

    private void Update() {
        timeStillLeft -= Time.deltaTime;

        UpdateSlider();
    }

    private void UpdateSlider() {
        slider.value = timeStillLeft;
    }

    public void PuzzlePieceAddTime() {
        timeStillLeft += timeToAdd;

        if (timeStillLeft >= startingTime) {
            timeStillLeft = startingTime;
        }
    }
}
