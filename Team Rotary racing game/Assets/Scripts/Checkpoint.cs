using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Index;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    LapManager LapManager1;

    private void Awake()
    {
        LapManager1 = GetComponentInParent<LapManager>();
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Car") == true)
        {
            //Debug.Log("Checkpoint " + Index);
            LapManager1.OnCheckpointPassed(Index);
        }
    }

    /*
    private void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            //m_CurrentLapStartTime -= Time.deltaTime;
        }
    }
    */
}
