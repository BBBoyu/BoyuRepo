using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapManagerUI : MonoBehaviour
{
    public Text LapTimeInfoText;

    LapManager m_LapManager;

    private void Awake()
    {
        m_LapManager = GetComponent<LapManager>();
    }

    void Update()
    {
        UpdateLapTimeInfoText();
    }

    void UpdateLapTimeInfoText()
    {
        LapTimeInfoText.text = "Current Lap: " + SecondsToTime(m_LapManager.CurrentLapTime) + "\n"
            + "Total Time: " + SecondsToTime(m_LapManager.TotalTime);

        //LapTimeInfoText.text = "Current Lap: " + SecondsToTime(m_LapManager.CurrentLapTime) + "\n"
        //+ "Last Lap: " + SecondsToTime(m_LapManager.LastLaptTime) + "\n"
        //+ "Best Lap: " + SecondsToTime(m_LapManager.BestLaptTime) + "";
    }

    string SecondsToTime(float seconds)
    {
        int Minutes = Mathf.FloorToInt(seconds / 60f);
        int Seconds = Mathf.FloorToInt(seconds % 60f);
        int Fraction = Mathf.FloorToInt((seconds - Seconds) * 100f);
        return Minutes + ":" + Seconds.ToString("00") + ":" + Fraction.ToString("00");
    }
}
