using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapUIHandler : MonoBehaviour
{
    [Header("Map prefab")]
    public GameObject mapPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    bool isChangingMap = false;

    MapData[] mapDatas;

    int selectedMapIndex = 0;

    //Other components
    //MapUIHandler mapUIHandler = null;

    // Start is called before the first frame update
    void Start()
    {
        //Load the car data
        mapDatas = Resources.LoadAll<MapData>("MapData/");

        StartCoroutine(SpawnMapCO(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnPreviousMap();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnNextMap();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectMap();
        }
    }

    public void OnPreviousMap()
    {
        if (isChangingMap)
            return;

        selectedMapIndex--;

        if (selectedMapIndex < 0)
            selectedMapIndex = mapDatas.Length - 1;

        StartCoroutine(SpawnMapCO(true));
    }

    public void OnNextMap()
    {
        if (isChangingMap)
            return;

        selectedMapIndex++;

        if (selectedMapIndex > mapDatas.Length - 1)
            selectedMapIndex = 0;

        StartCoroutine(SpawnMapCO(false));
    }

    public void OnSelectMap()
    {
        PlayerPrefs.SetInt("P1SelectedMapID", mapDatas[selectedMapIndex].mapIndex);
        //Multiple player input selection
        //PlayerPrefs.SetInt("P2SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        //PlayerPrefs.SetInt("P3SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        //PlayerPrefs.SetInt("P4SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);

        PlayerPrefs.Save();

        SceneManager.LoadScene("SpawnCar");
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    

    
    IEnumerator SpawnMapCO(bool isCarAppearingOnRightSide)
    {
        /*
        isChangingMap = true;

        if (carUIHandler != null)
            carUIHandler.StartCarExitAnimation(!isCarAppearingOnRightSide);

        GameObject instantiatedCar = Instantiate(mapPrefab, spawnOnTransform);

        carUIHandler = instantiatedCar.GetComponent<CarUIHandler>();
        carUIHandler.SetupCar(carDatas[selectedCarIndex]);
        carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.4f);
        isChangingCar = false;

        */
    }
    
    
}
