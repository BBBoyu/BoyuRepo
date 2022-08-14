using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapManagerUI : MonoBehaviour
{
    public Text LapTimeInfoText;

    LapManager lapManager;

    private void Awake()
    {
        lapManager = GetComponent<LapManager>();
    }

    void Update()
    {
        UpdateLapTimeInfoText();
    }

    void UpdateLapTimeInfoText()
    {
        LapTimeInfoText.text = "Current Lap: " + SecondsToTime(lapManager.CurrentLapTime) + "\n"
            + "Total Time: " + SecondsToTime(lapManager.TotalTime);
    }

    string SecondsToTime(float seconds)
    {
        int Minutes = Mathf.FloorToInt(seconds / 60f);
        int Seconds = Mathf.FloorToInt(seconds % 60f);
        int Fraction = Mathf.FloorToInt((seconds - Seconds) * 100f);
        return Minutes + ":" + Seconds.ToString("00") + ":" + Fraction.ToString("00");
    }
}
