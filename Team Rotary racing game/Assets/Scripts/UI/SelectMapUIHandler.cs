using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMapUIHandler : MonoBehaviour
{
    [Header("Map prefab")]
    public GameObject mapPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    public Text mapName;
    public Text mapDescription;

    bool isChangingMap = false;

    public static string mapToLoad;

    MapData[] mapDatas;

    int selectedMapIndex = 0;


    MapUIHandler mapUIHandler = null;

    void Start()
    {
        //Load the car data
        mapDatas = Resources.LoadAll<MapData>("MapData/");
        Debug.Log(mapDatas.Length);

        StartCoroutine(SpawnMapCO(true));
    }

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
        PlayerPrefs.SetInt("P1SelectedMapID", mapDatas[selectedMapIndex].MapIndex);

        PlayerPrefs.Save();
        mapToLoad = mapDatas[selectedMapIndex].sceneToLoad;
        SceneManager.LoadScene("Car Selection");
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    

    
    IEnumerator SpawnMapCO(bool isCarAppearingOnRightSide)
    {
        
        isChangingMap = true;

        if (mapUIHandler != null)
            mapUIHandler.StartMapExitAnimation(!isCarAppearingOnRightSide);

        
        GameObject instantiatedMap = Instantiate(mapPrefab, spawnOnTransform);

        mapUIHandler = instantiatedMap.GetComponent<MapUIHandler>();

        DisplayMap(mapDatas[selectedMapIndex]);
        mapUIHandler.SetupMap(mapDatas[selectedMapIndex]);
        mapUIHandler.StartMapEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.2f);
        isChangingMap = false;


    }

    public void DisplayMap(MapData mapdata)
    {
        mapName.text = mapdata.mapName;
        mapDescription.text = mapdata.mapDescription;
    }


}
