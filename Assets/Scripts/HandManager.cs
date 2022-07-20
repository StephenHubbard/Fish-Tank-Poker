using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{

    [SerializeField] private GameObject[] cardBoxes;
    [SerializeField] private Sprite[] suitSprites;
    [SerializeField] private Sprite uiMask;

    private HistogramManager histogramManager;

    private void Awake() {
        histogramManager = FindObjectOfType<HistogramManager>();
    }

    private void Start() {
        ResetHand();
    }

    public void SetHand(string fishSuit, int fishValue) {
        for (int i = 0; i < cardBoxes.Length; i++)
        {
            if (cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text.Length == 0) {
                AssignTextValue(fishValue, i);
                AssignSuitSprite(fishSuit, i);

                histogramManager.UpdateValueHistogram(fishValue);
                histogramManager.UpdateSuitHistogram(fishSuit);
                histogramManager.UpdateComboHistogram(fishValue, fishSuit);

                if (cardBoxes[4].GetComponentInChildren<TextMeshProUGUI>().text.Length > 0) {
                    DetectHandStrength();
                    ResetHand();
                }

                return;
            }
        }
    }

    private void AssignTextValue(int fishValue, int i) {
        if (fishValue > 1 && fishValue < 11) {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = fishValue.ToString();
        } else if (fishValue == 11) {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "J";
        } else if (fishValue == 12) {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "Q";
        } else if (fishValue == 13) {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "K";
        } else if (fishValue == 1) {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "A";
        }
    }

    private void AssignSuitSprite(string fishSuit, int i) {
        if (fishSuit == "c") {
            cardBoxes[i].transform.Find("SuitImage").GetComponent<Image>().sprite = suitSprites[0];
        }

        if (fishSuit == "d") {
            cardBoxes[i].transform.Find("SuitImage").GetComponent<Image>().sprite = suitSprites[1];
        }

        if (fishSuit == "s") {
            cardBoxes[i].transform.Find("SuitImage").GetComponent<Image>().sprite = suitSprites[2];
        }

        if (fishSuit == "h") {
            cardBoxes[i].transform.Find("SuitImage").GetComponent<Image>().sprite = suitSprites[3];
        }

    }

    private void DetectHandStrength() {
        if (histogramManager.CheckForQuads()) {
            print("Quads");
            return;
        }

        if (histogramManager.CheckForBoat()) {
            print("Full House");
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

    public void ResetHand() {
        for (int i = 0; i < cardBoxes.Length; i++)
        {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            cardBoxes[i].transform.Find("SuitImage").GetComponent<Image>().sprite = uiMask;
        }

        histogramManager.ClearHistograms();
    }

    
}
