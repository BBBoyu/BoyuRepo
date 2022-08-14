using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Index;

    void Start()
    {
        
    }


    LapManager lapManager;

    private void Awake()
    {
        lapManager = GetComponentInParent<LapManager>();
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Car") == true)
        {
            lapManager.OnCheckpointPassed(Index);
        }
        else if (otherCollider.CompareTag("AI") == true)
        {
            Debug.Log("Checkpoint " + Index + " " +otherCollider.tag);
            lapManager.aiOnCheckpointPassed(Index);
        }
    }

}
