using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController__update : MonoBehaviour
{
    public static GameController__update instance;
    public GameObject hudcontainer;
    public Text timeCounter;
    public Text LapTimeInfoText;
    public Text countdownText;
    public int countdownTime;
    LapManager m_LapManager;

    private float startTime, elapsedTime;
    public bool gamePlaying { get; private set; }
    TimeSpan timePlaying;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        gamePlaying = false;

        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;
        m_LapManager = GetComponent<LapManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gamePlaying)
        {
            UpdateLapTimeInfoText();
        }
    }

    void UpdateLapTimeInfoText()
    {
        LapTimeInfoText.text = "Current Lap: " + SecondsToTime(m_LapManager.CurrentLapTime) + "\n"
            + "Total Time: " + SecondsToTime(m_LapManager.TotalTime);
    }


    string SecondsToTime(float seconds)
    {
        int Minutes = Mathf.FloorToInt(seconds / 60f);
        int Seconds = Mathf.FloorToInt(seconds % 60f);
        int Fraction = Mathf.FloorToInt((seconds - Seconds) * 100f);
        return Minutes + ":" + Seconds.ToString("00") + ":" + Fraction.ToString("00");
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        BeginGame();

        countdownText.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }
}
