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
                return Time.realtimeSinceStartup - 6;
            }
            else if (count == 0)
            {
                return 0f;
            }
            return Time.realtimeSinceStartup - 6 - m_CurrentLapStartTime;
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
            return Time.realtimeSinceStartup - 6;
        }
    }

    public float LastLaptTime { get; private set; }
    public float BestLaptTime { get; private set; }

    public bool m_IsLapStarted = false;
    public float m_CurrentLapStartTime;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Car") == true)
        {
            if (m_IsLapStarted == true)
            {
                LastLaptTime = Time.realtimeSinceStartup - 6 - m_CurrentLapStartTime;

                if (LastLaptTime < BestLaptTime || BestLaptTime == 0f)
                {
                    BestLaptTime = LastLaptTime;
                }
            }
        }
        count++;
        m_IsLapStarted = true;
        m_CurrentLapStartTime = Time.realtimeSinceStartup - 6;
    }
}
