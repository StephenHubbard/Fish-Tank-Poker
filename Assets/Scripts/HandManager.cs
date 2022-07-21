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
    [SerializeField] private TMP_Text handClassText;

    private HistogramManager histogramManager;
    private PointManager pointManager;

    private void Awake() {
        pointManager = FindObjectOfType<PointManager>();
        histogramManager = FindObjectOfType<HistogramManager>();
    }

    private void Start() {
        ResetHand();
        handClassText.gameObject.SetActive(false);
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
            pointManager.AddPoints(100);
            HandClassAnimation("Quads");
            return;
        }

        if (histogramManager.CheckForBoat()) {
            pointManager.AddPoints(80);
            HandClassAnimation("Full House");
            return;
        }

        if (histogramManager.CheckForTrips()) {
            pointManager.AddPoints(40);
            HandClassAnimation("Trips");
            return;
        }

        if (histogramManager.CheckForTwoPair()) {
            pointManager.AddPoints(20);
            HandClassAnimation("Two Pair");
            return;
        }

        if (histogramManager.CheckForOnePair()) {
            pointManager.AddPoints(10);
            HandClassAnimation("One Pair");
            return;
        }

        if (histogramManager.CheckForStraightFlush()) {
            pointManager.AddPoints(250);
            HandClassAnimation("Straight Flush");
            return;
        }

        if (histogramManager.CheckForFlush()) {
            pointManager.AddPoints(60);
            HandClassAnimation("Flush");
            return;
        }

        if (histogramManager.CheckForStraight()) {
            pointManager.AddPoints(50);
            HandClassAnimation("Straight");
            return;
        }

        HandClassAnimation("Try Again");
    }

    public void RepeatCardText() {
        HandClassAnimation("Repeat Card");
    }

    private void HandClassAnimation(string handClassStr) {
        handClassText.gameObject.SetActive(true);
        handClassText.text = handClassStr;
        StartCoroutine(HandClassCo());
    }

    private IEnumerator HandClassCo() {
        yield return new WaitForSeconds(2f);
        handClassText.gameObject.SetActive(false);
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
