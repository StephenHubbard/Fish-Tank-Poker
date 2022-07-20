using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    private HandManager handManager;
    private AudioManager audioManager;

    private void Awake() {
        handManager = FindObjectOfType<HandManager>();
        audioManager = FindObjectOfType<AudioManager>();
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


    [SerializeField] private SuitValue suitValue;
    [SerializeField] private FishValue fishValue;


    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            handManager.SetHand(suitValue.ToString(), (int)fishValue);
            audioManager.EatFishCrunch();
            Destroy(gameObject);
        }
    }
}
