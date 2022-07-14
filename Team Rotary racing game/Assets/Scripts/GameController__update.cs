using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController__update : MonoBehaviour
{
    public static GameController__update instance;
    public GameObject hudcontainer, GameOverPanel;
    public Text timeCounter;
    public Text LapTimeInfoText;
    public Text countdownText;
    public int countdownTime;
    public int targetLaps;
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
            if (m_LapManager.count > targetLaps || m_LapManager.aicount > targetLaps)
            {
                EndGame();
            }
        }
    }

    void UpdateLapTimeInfoText()
    {
        if (m_LapManager.count <= targetLaps && m_LapManager.aicount <= targetLaps)
        {
            LapTimeInfoText.text = "Current Lap: " + SecondsToTime(m_LapManager.CurrentLapTime) + "\n"
                + "Lap Count: " + m_LapManager.count + "/" + targetLaps + "\n"
                + "Total Time: " + SecondsToTime(m_LapManager.TotalTime) + "\n"
                + "AI Lap Count: " + m_LapManager.aicount + "/" + targetLaps;

        }
        
    }


    string SecondsToTime(float seconds)
    {
        int Minutes = Mathf.FloorToInt(seconds / 60f);
        int Seconds = Mathf.FloorToInt(seconds % 60f);
        int Fraction = Mathf.FloorToInt((seconds - Seconds - Minutes * 60) * 100f);
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

    private void EndGame()
    {
        Debug.Log(SelectModeHandler.mode);
        Debug.Log(SelectModeHandler.mode == "AIRacer");
        if (SelectModeHandler.mode == "AIRacer")
        {
            gamePlaying = false;
            Invoke("ShowGameOverScreen", 0.25f);
        }
        else
        {
            gamePlaying = false;
            Invoke("ShowGameOverScreen", 0.25f);
        }

    }

    private void ShowGameOverScreen()
    {
        if (SelectModeHandler.mode == "TimeAttack")
        {
            GameOverPanel.SetActive(true);
            hudcontainer.SetActive(false);
            string timePlayingStr = "Total time:" + SecondsToTime(m_LapManager.TotalTime);
            GameOverPanel.transform.Find("TimeDisplay").GetComponent<TMPro.TextMeshProUGUI>().text = timePlayingStr;
        }
        else if (SelectModeHandler.mode == "AIRacer")
        {
            GameOverPanel.SetActive(true);
            hudcontainer.SetActive(false);
            string result;
            if (m_LapManager.won)
            {
                result = "Won";
            } else
            {
                result = "Lost";
            }

            string timePlayingStr = "You have " + result;
            GameOverPanel.transform.Find("TimeDisplay").GetComponent<TMPro.TextMeshProUGUI>().text = timePlayingStr;
        }
        
    }

    public void OnButtonLoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
