using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandManager : MonoBehaviour
{

    [SerializeField] private GameObject[] cardBoxes;

    private HistogramManager histogramManager;

    private void Awake() {
        histogramManager = FindObjectOfType<HistogramManager>();
    }

    public void SetHand(string fishSuit, int fishValue) {
        for (int i = 0; i < cardBoxes.Length; i++)
        {
            if (cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text.Length == 0) {
                cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = fishValue + fishSuit;

                histogramManager.UpdateValueHistogram(fishValue);
                histogramManager.UpdateSuitHistogram(fishSuit);

                if (cardBoxes[4].GetComponentInChildren<TextMeshProUGUI>().text.Length > 0) {
                    DetectHandStrength();
                    ResetHand();
                }


                return;
            }
        }
    }

    private void DetectHandStrength() {
        if (histogramManager.CheckForQuads()) {
            print("Quads");
            return;
        }

        if (histogramManager.CheckForBoat()) {
            print("Boat");
            return;
        }

        if (histogramManager.CheckForTrips()) {
            print("Trips");
            return;
        }

        if (histogramManager.CheckForTwoPair()) {
            print("Two Pair");
            return;
        }

        if (histogramManager.CheckForOnePair()) {
            print("One Pair");
            return;
        }

        if (histogramManager.CheckForStraightFlush()) {
            print("Straight Flush");
            return;
        }

        if (histogramManager.CheckForFlush()) {
            print("Flush");
            return;
        }

        if (histogramManager.CheckForStraight()) {
            print("Straight");
            return;
        }

        print("nada");
    }

    private void ResetHand() {
        for (int i = 0; i < cardBoxes.Length; i++)
        {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        histogramManager.ClearValueHistogram();
        histogramManager.ClearSuitHistogram();
    }

    
}
