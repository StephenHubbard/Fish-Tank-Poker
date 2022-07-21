using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fish : MonoBehaviour
{
    [SerializeField] private AudioClip[] fishCrunchSFX;
    [SerializeField] private SuitValue suitValue;
    [SerializeField] private FishValue fishValue;
    [SerializeField] private TMP_Text valueText;

    private HandManager handManager;

    private void Awake() {
        handManager = FindObjectOfType<HandManager>();
    }

    private void Start() {
        fishValue = (FishValue)Random.Range(1, 13);

        if ((int)fishValue >= 2 && (int)fishValue <= 9) {
            int fishInt = (int)fishValue;
            valueText.text = fishInt.ToString();
        } else {
            valueText.text = fishValue.ToString();
        }
    }

    public enum SuitValue 
    {
        c, 
        d, 
        h, 
        s
    };

    public enum FishValue
    {
        A = 1,
        Deuce = 2,
        Three = 3, 
        Four = 4, 
        Five = 5,
        Six = 6,
        Seven = 7, 
        Eight = 8, 
        Nine = 9, 
        T = 10,
        J = 11, 
        Q = 12, 
        K = 13,
    };
    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            handManager.SetHand(suitValue.ToString(), (int)fishValue);
            AudioSource.PlayClipAtPoint(fishCrunchSFX[Random.Range(0, fishCrunchSFX.Length)], Camera.main.gameObject.transform.position, 1f);
            Destroy(gameObject);
        }
    }
}
