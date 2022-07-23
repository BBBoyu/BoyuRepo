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
    public int targetTime;
    public float best_track1;
    public float best_track2;
    public float best_track3;
    public float best_track4;
    public float personalBest;
    public float personalBestDisplay;
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

        best_track1 = PlayerPrefs.GetFloat("PB1", 9999);

        best_track2 = PlayerPrefs.GetFloat("PB2", 9999);

        best_track3 = PlayerPrefs.GetFloat("PB3", 9999);

        best_track4 = PlayerPrefs.GetFloat("PB4", 9999);

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
        if (SelectModeHandler.mode == "TimeAttack")
        {
            if (SceneManager.GetActiveScene().name == "track1")
            {
                targetTime = 75;
                personalBest = best_track1;
            }
            else if (SceneManager.GetActiveScene().name == "track2")
            {
                targetTime = 65;
                personalBest = best_track2;
            }
            else if (SceneManager.GetActiveScene().name == "track3")
            {
                targetTime = 70;
                personalBest = best_track3;
            }

            else if (SceneManager.GetActiveScene().name == "track4")
            {
                targetTime = 80;
                personalBest = best_track4;
            }
            if (personalBest == 9999)
            {
                personalBestDisplay = 0;
            }
            else
            {
                personalBestDisplay = personalBest;
            }
            if (m_LapManager.count <= targetLaps)
            {
                LapTimeInfoText.text = "Current Lap: " + SecondsToTime(m_LapManager.CurrentLapTime) + "\n"
                    + "Lap Count: " + m_LapManager.count + "/" + targetLaps + "\n"
                    + "Total Time: " + SecondsToTime(m_LapManager.TotalTime) + "\n"
                    + "Target Time:" + SecondsToTime(targetTime) + "\n"
                    + "Personal Best:" + SecondsToTime(personalBestDisplay);

            }
        }
        else if (SelectModeHandler.mode == "AIRacer")
        {
            if (m_LapManager.count <= targetLaps && m_LapManager.aicount <= targetLaps)
            {
                LapTimeInfoText.text = "Current Lap: " + SecondsToTime(m_LapManager.CurrentLapTime) + "\n"
                    + "Lap Count: " + m_LapManager.count + "/" + targetLaps + "\n"
                    + "Total Time: " + SecondsToTime(m_LapManager.TotalTime) + "\n"
                    + "AI Lap Count: " + m_LapManager.aicount + "/" + targetLaps;

            }
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
            SavePersonalBest();
        }
        else
        {
            gamePlaying = false;
            Invoke("ShowGameOverScreen", 0.25f);
            SavePersonalBest();
        }

    }

    private void SavePersonalBest()
    {
        if (m_LapManager.TotalTime <= personalBest)
        {
            personalBest = m_LapManager.TotalTime;
            if (SceneManager.GetActiveScene().name == "track1")
            {
                PlayerPrefs.SetFloat("PB1", personalBest);
            }
            if (SceneManager.GetActiveScene().name == "track2")
            {
                PlayerPrefs.SetFloat("PB2", personalBest);
            }
            if (SceneManager.GetActiveScene().name == "track3")
            {
                PlayerPrefs.SetFloat("PB3", personalBest);
            }
            if (SceneManager.GetActiveScene().name == "track4")
            {
                PlayerPrefs.SetFloat("PB4", personalBest);
            }
            PlayerPrefs.Save();
        }
    }

    private void ShowGameOverScreen()
    {
        string cheer_up;
        if (SelectModeHandler.mode == "TimeAttack")
        {
            GameOverPanel.SetActive(true);
            hudcontainer.SetActive(false);
            string timePlayingStr = "Total time:" + SecondsToTime(m_LapManager.TotalTime);
            if (m_LapManager.TotalTime <= targetTime)
            {
                cheer_up = "  Congratulations!";
                GameOverPanel.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = cheer_up;
            }
            else if (m_LapManager.TotalTime >= targetTime)
            {
                cheer_up = "Can you do better?";
                GameOverPanel.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = cheer_up;
            }
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
                cheer_up = "  Congratulations!";
            } 
            else
            {
                result = "Lost";
                cheer_up = "Can you do better?";
            }

            string timePlayingStr = "    You have " + result;
            GameOverPanel.transform.Find("TimeDisplay").GetComponent<TMPro.TextMeshProUGUI>().text = timePlayingStr;
            GameOverPanel.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = cheer_up;
        }
        
    }

    public void OnButtonLoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
