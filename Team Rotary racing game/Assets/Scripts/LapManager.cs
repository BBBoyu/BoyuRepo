using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    public static int count = 0;
    public float CurrentLapTime
    {
        get
        {
            if (count == 1)
            {
                return Time.time - m_CurrentLapStartTime;
            }
            else if (count == 0)
            {
                return 0f;
            }
            return Time.time - m_CurrentLapStartTime;
        }
    }

    public float TotalTime
    {
        get
        {
            if (m_IsLapStarted == false)
            {
                return 0f;
            }
            return Time.time - m_StartTime;
        }
    }

    public float LastLaptTime { get; private set; }
    public float BestLaptTime { get; private set; }

    public bool m_IsLapStarted = false;
    public float m_CurrentLapStartTime;
    public float m_StartTime;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Car") == true)
        {
            if (m_IsLapStarted == true)
            {
                LastLaptTime = Time.time - m_CurrentLapStartTime;

                if (LastLaptTime < BestLaptTime || BestLaptTime == 0f)
                {
                    BestLaptTime = LastLaptTime;
                }
            }
        }
        count++;
        m_IsLapStarted = true;
        m_CurrentLapStartTime = Time.time;

        if (count == 1)
        {
            m_StartTime = Time.time;
        }
    }

    private void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            m_CurrentLapStartTime -= Time.deltaTime;
        }
    }
}
