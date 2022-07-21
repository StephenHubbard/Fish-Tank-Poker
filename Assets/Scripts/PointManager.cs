using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int currentPoints;

    private void Start() {
        currentPoints = 0;
    }

    private void Update() {
        scoreText.text = "Score: " + currentPoints.ToString();
    }

    public void AddPoints(int points) {
        currentPoints += points;
    }

}
