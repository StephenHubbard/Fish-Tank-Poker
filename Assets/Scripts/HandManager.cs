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
    [SerializeField] private AudioClip[] HandClassSFX;

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
                if (cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text.Length == 0)
                {
                    AssignTextValue(fishValue, i);
                    AssignSuitSprite(fishSuit, i);

                    histogramManager.UpdateValueHistogram(fishValue);
                    histogramManager.UpdateSuitHistogram(fishSuit);
                    histogramManager.UpdateComboHistogram(fishValue, fishSuit);

                    CheckForSwimmingFishInHand();

                    if (cardBoxes[4].GetComponentInChildren<TextMeshProUGUI>().text.Length > 0)
                    {
                    DetectHandStrength();
                    ResetHand();
                    }

                    return;
                }
            }
        }

    private static void CheckForSwimmingFishInHand()
    {
        Fish[] allFish = FindObjectsOfType<Fish>();

        foreach (var item in allFish)
        {
            item.GetComponent<Fish>().CheckForRepeatSwimmingFish();
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
            AudioSource.PlayClipAtPoint(HandClassSFX[6], Camera.main.gameObject.transform.position, .3f);

            return;
        }

        if (histogramManager.CheckForBoat()) {
            pointManager.AddPoints(80);
            HandClassAnimation("Full House");
            AudioSource.PlayClipAtPoint(HandClassSFX[5], Camera.main.gameObject.transform.position, .3f);
            return;
        }

        if (histogramManager.CheckForTrips()) {
            pointManager.AddPoints(40);
            HandClassAnimation("Trips");
            AudioSource.PlayClipAtPoint(HandClassSFX[2], Camera.main.gameObject.transform.position, .3f);
            return;
        }

        if (histogramManager.CheckForTwoPair()) {
            pointManager.AddPoints(20);
            HandClassAnimation("Two Pair");
            AudioSource.PlayClipAtPoint(HandClassSFX[1], Camera.main.gameObject.transform.position, .3f);
            return;
        }

        if (histogramManager.CheckForOnePair()) {
            pointManager.AddPoints(10);
            HandClassAnimation("One Pair");
            AudioSource.PlayClipAtPoint(HandClassSFX[0], Camera.main.gameObject.transform.position, .3f);
            return;
        }

        if (histogramManager.CheckForStraightFlush()) {
            pointManager.AddPoints(250);
            HandClassAnimation("Straight Flush");
            AudioSource.PlayClipAtPoint(HandClassSFX[7], Camera.main.gameObject.transform.position, .3f);
            return;
        }

        if (histogramManager.CheckForFlush()) {
            pointManager.AddPoints(60);
            HandClassAnimation("Flush");
            AudioSource.PlayClipAtPoint(HandClassSFX[4], Camera.main.gameObject.transform.position, .3f);
            return;
        }

        if (histogramManager.CheckForStraight()) {
            pointManager.AddPoints(50);
            HandClassAnimation("Straight");
            AudioSource.PlayClipAtPoint(HandClassSFX[3], Camera.main.gameObject.transform.position, .3f);
            return;
        }

        HandClassAnimation("Try Again");
        AudioSource.PlayClipAtPoint(HandClassSFX[8], Camera.main.gameObject.transform.position, .4f);

    }

    public void RepeatCardText() {
        AudioSource.PlayClipAtPoint(HandClassSFX[8], Camera.main.gameObject.transform.position, .4f);
        HandClassAnimation("Repeat Card");
    }

    private void HandClassAnimation(string handClassStr) {
        handClassText.gameObject.SetActive(true);
        handClassText.gameObject.GetComponent<Animator>().SetTrigger("NewHand");
        handClassText.text = handClassStr;
        StartCoroutine(HandClassCo());
    }

    private IEnumerator HandClassCo() {
        yield return new WaitForSeconds(1f);
        handClassText.gameObject.SetActive(false);
    }

    public void ResetHand() {
        for (int i = 0; i < cardBoxes.Length; i++)
        {
            cardBoxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            cardBoxes[i].transform.Find("SuitImage").GetComponent<Image>().sprite = uiMask;
        }

        histogramManager.ClearHistograms();

        Fish[] allFish = FindObjectsOfType<Fish>();

        foreach (var item in allFish)
        {
            item.GetComponent<Fish>().StopFlashing();
        }
    }

    
}
