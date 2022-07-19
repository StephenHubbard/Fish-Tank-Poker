using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class HistogramManager : MonoBehaviour
{
    // Num Value

    [SerializeField] int[] numValue = new int[] {};

    SortedDictionary<int, int> valueHistogram = new SortedDictionary<int, int>();

    public void UpdateValueHistogram(int fishIntValue)
    {
        numValue = numValue.Concat(new int[] { fishIntValue }).ToArray();

        valueHistogram = new SortedDictionary<int, int>();

        foreach (int item in numValue) {
            if (valueHistogram.ContainsKey(item)) {
                valueHistogram[item]++;
            } else {
                valueHistogram[item] = 1;
            }
        }
        
        // foreach (KeyValuePair<int, int> pair in valueHistogram) {
        //     string testString = "There are " + pair.Value + " " + pair.Key + "'s";
        //     print(testString);
        // }
    }

    private void CheckIfRepeat() {
        
    }

    public void ClearValueHistogram() {
        numValue = new int[] {};
    }

    // Suits 

    [SerializeField] string[] suitValue = new string[] {};

    SortedDictionary<string, int> suitHistogram = new SortedDictionary<string, int>();

    public void UpdateSuitHistogram(string fishSuitValue)
    {
        suitValue = suitValue.Concat(new string[] { fishSuitValue }).ToArray();

        suitHistogram = new SortedDictionary<string, int>();

        foreach (string item in suitValue) {
            if (suitHistogram.ContainsKey(item)) {
                suitHistogram[item]++;
            } else {
                suitHistogram[item] = 1;
            }
        }
        
        // foreach (KeyValuePair<string, int> pair in suitHistogram) {
        //     string testString = "There are " + pair.Value + " " + pair.Key;
        //     print(testString);
        // }
    }

    public void ClearSuitHistogram() {
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

        print(isFlush);
        print(isStraight);

        if (isStraight && isFlush) {
            return true;
        } else {
            return false;
        }
    }
}
