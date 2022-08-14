using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        CarData[] carDatas = Resources.LoadAll<CarData>("CarData/");

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i].transform;

            int playerSelectedCarID = PlayerPrefs.GetInt($"P{i + 1}SelectedCarID");

            foreach (CarData cardata in carDatas)
            {
                if (cardata.CarUniqueID == playerSelectedCarID)
                {
                    GameObject playerCar = Instantiate(cardata.CarPrefab, spawnPoint.position, spawnPoint.rotation);

                    playerCar.GetComponent<CarInputHandler>().playerNumber = i + 1;

                    break;
                }
            }
        }
        if (SelectModeHandler.mode == "AIRacer")
        {
            GameObject[] aiPoints = GameObject.FindGameObjectsWithTag("AISpawnPoint");
            Transform aiPoint = aiPoints[0].transform;
            AICar[] AIdata = Resources.LoadAll<AICar>("AICar/");
            GameObject aiCar = Instantiate(AIdata[0].CarPrefab, aiPoint.position, aiPoint.rotation);
        }
        
    }

}
