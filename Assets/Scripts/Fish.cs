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
    [SerializeField] private GameObject fishLight;

    private HandManager handManager;
    private HistogramManager histogramManager;

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

    public SuitValue suitVal;
    public FishValue fishVal;

    private void Awake() {
        handManager = FindObjectOfType<HandManager>();
        histogramManager = FindObjectOfType<HistogramManager>();
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

    // public void CheckForRepeatSwimmingFish(int fishIntValue, string fishSuitValue) {
    //     Fish[] allFish = FindObjectsOfType<Fish>();

    //     foreach (var item in allFish)
    //     {
    //         // print(item.GetComponent<Fish>().suitVal.ToString());
    //         // print(fishSuitValue);
    //         // print((int)item.GetComponent<Fish>().fishVal);
    //         // print(fishIntValue);

    //         if ((item.GetComponent<Fish>().suitVal.ToString() == fishSuitValue) && (int)item.GetComponent<Fish>().fishVal == fishIntValue) {
    //             item.GetComponent<Fish>().ToggleFlashingFish();
    //         } else {
    //             continue;
    //         }
    //     }
    // }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            handManager.SetHand(suitValue.ToString(), (int)fishValue);
            AudioSource.PlayClipAtPoint(fishCrunchSFX[Random.Range(0, fishCrunchSFX.Length)], Camera.main.gameObject.transform.position, 1f);
            Destroy(gameObject);
        }
    }

    public void ToggleFlashingFish() {
        fishLight.GetComponent<Animator>().SetBool("FishInUse", true);
    }
}
