using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{

    private void Awake()
    {
        startTime = Time.timeSinceLevelLoad + 3;
    }

    public float startTime;
    public float SinceStartTime;

    public int count = 0;
    public int aicount = 0;
    public bool won;

    public float CurrentLapTime
    {
        get
        {
            
            if (count == 0)
            {
                return 0f;
            }

            else if (count == 1)
            {
                //return TotalTime;
                return Time.time - startTime;
            }


            return Time.time - m_CurrentLapStartTime;
        }
    }

    public float TotalTime
    {
        get
        {
            if (count == 0)
            {
                return 0f;
            }
           return Time.time - startTime;
        }
    }



    public bool m_IsLapStarted = false;
    public float m_CurrentLapStartTime;
    int LastCheckpointIndex = 0;
    public int CheckPointCount;

    int aiLastCheckpointIndex = 0;
    public int aiCheckPointCount;


    private void Start() {
        CheckPointCount = GetCheckPointCount();
    }

    int GetCheckPointCount()
    {
        int checkpointcount = 0;
        Checkpoint[] checkpoints = GetComponentsInChildren<Checkpoint>();

        for (int i = 0; i < checkpoints.Length; ++i)
        {
            checkpointcount = Mathf.Max(CheckPointCount, checkpoints[i].Index);
        }
        return checkpointcount;
    }
    

    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            m_CurrentLapStartTime -= Time.deltaTime;
        }
    }

    

    public void OnCheckpointPassed(int index)
    {
        if (index == 0)
            {
                if (LastCheckpointIndex == CheckPointCount || m_IsLapStarted == false )
                {
                    //Debug.Log("Checkpoint " + index);
                    OnFinishLineePassed();
                }
            }
            
        else
        {
            if (index == LastCheckpointIndex +1)
            {
                //Debug.Log("Checkpoint " + index);
                LastCheckpointIndex = index;
            }
        }

    }

    public void aiOnCheckpointPassed(int index)
    {
        if (index == 0)
        {
            if (aiLastCheckpointIndex == CheckPointCount || m_IsLapStarted == false)
            {
                //Debug.Log("Checkpoint " + index);
                aiOnFinishLineePassed();
            }
        }

        else
        {
            if (index == aiLastCheckpointIndex + 1)
            {
                //Debug.Log("Checkpoint " + index);
                aiLastCheckpointIndex = index;
            }
        }

    }

    void OnFinishLineePassed()
    {

        count++;
        m_IsLapStarted = true;
        m_CurrentLapStartTime = Time.time;
        LastCheckpointIndex = 0;
        won = count > aicount;
        
        if (count == 1)
        {
            startTime = Time.time;
        }
        
    }

    void aiOnFinishLineePassed()
    {

        aicount++;
        aiLastCheckpointIndex = 0;
        won = count > aicount;

    }
    

}
