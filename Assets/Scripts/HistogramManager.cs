using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class HistogramManager : MonoBehaviour
{
    // Num Value

    [SerializeField] private AudioClip repeatCardSFX;
    [SerializeField] int[] numValue = new int[] {};
    [SerializeField] string[] suitValue = new string[] {};
    [SerializeField] string[] cardCombo = new string[] {};

    private HandManager handManager;

    private void Awake() {
        handManager = FindObjectOfType<HandManager>();
    }

    public void UpdateValueHistogram(int fishIntValue)
    {
        numValue = numValue.Concat(new int[] { fishIntValue }).ToArray();
    }

    // Suits 

    public void UpdateSuitHistogram(string fishSuitValue)
    {
        suitValue = suitValue.Concat(new string[] { fishSuitValue }).ToArray();
    }

    // Combo

    public void UpdateComboHistogram(int fishIntValue, string fishSuitValue)
    {
        string fishIntStr = fishIntValue.ToString();
        string comboStr = fishIntStr + fishSuitValue;

        cardCombo = cardCombo.Concat(new string[] { comboStr }).ToArray();

        CheckIfRepeat();
    }


    private void CheckIfRepeat() {
        var counts = cardCombo.GroupBy(i => i);

            foreach (var group in counts)
            {
                if (group.Count() > 1) {
                    AudioSource.PlayClipAtPoint(repeatCardSFX, Camera.main.gameObject.transform.position, 1f);
                    handManager.RepeatCardText();
                    handManager.ResetHand();
                } 
            }
    }

    public void ClearHistograms() {
        cardCombo = new string[] {};
        numValue = new int[] {};
        suitValue = new string[] {};
    }

    // Hand strength

    public bool CheckForQuads() {
        var counts = numValue.GroupBy(i => i);

        foreach (var group in counts)
        {
            if (group.Count() == 4) {
                return true;
            } 
        }
        return false;
    }

    public bool CheckForBoat() {
        var counts = numValue.GroupBy(i => i);
        bool hasTrips = false;
        bool hasPair = false;

        foreach (var group in counts)
        {
            if (group.Count() == 3) {
                hasTrips = true;
            } 

            if (group.Count() == 2) {
                hasPair = true;
            } 
        }

        if (hasTrips && hasPair) { 
            return true;
        } else { 
            return false;
        }
    }

    public bool CheckForTrips() {
        var counts = numValue.GroupBy(i => i);

        foreach (var group in counts)
        {
            if (group.Count() == 3) {
                return true;
            } 
        }
        return false;
    }

    public bool CheckForTwoPair() {
        var counts = numValue.GroupBy(i => i);
        bool hasOnePair = false;
        bool hasTwoPair = false;

        foreach (var group in counts)
        {
            if (group.Count() == 2 && hasOnePair) {
                hasTwoPair = true;
            } 

            if (group.Count() == 2) {
                hasOnePair = true;
            } 
        }

        if (hasOnePair && hasTwoPair) { 
            return true;
        } else { 
            return false;
        }
    }

    public bool CheckForOnePair() {
        var counts = numValue.GroupBy(i => i);

        foreach (var group in counts)
        {
            if (group.Count() == 2) {
                return true;
            } 
        }
        return false;
    }

    public bool CheckForFlush() {
        var counts = suitValue.GroupBy(i => i);

        foreach (var group in counts)
        {
            if (group.Count() == 5) {
                return true;
            } 
        }
        return false;
    }

    public bool CheckForStraight() {
        Array.Sort(numValue);

        if (numValue[0] == 1 && numValue[4] == 13) {
            return true;
        }

        if (numValue[4] - numValue[0] == 4) {
            return true;
        }
        
        return false;
    }

    public bool CheckForStraightFlush() {
        var counts = suitValue.GroupBy(i => i);

        bool isFlush = false;
        bool isStraight = false;

        foreach (var group in counts)
        {
            if (group.Count() == 5) {
                isFlush = true;
            } 
        }

        Array.Sort(numValue);

        if ((numValue[0] == 1 && numValue[4] == 13) || (numValue[4] - numValue[0] == 4)) {
            isStraight = true;
        }

        if (isStraight && isFlush) {
            return true;
        } else {
            return false;
        }
    }
}
