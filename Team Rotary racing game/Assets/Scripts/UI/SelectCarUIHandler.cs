using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCarUIHandler : MonoBehaviour
{
    [Header("Car prefab")]
    public GameObject carPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    [Header("Car Stats")]
    public Text carName;
    public Image speed;
    public Image acc;
    public Image handling;


    bool isChangingCar = false;

    CarData[] carDatas;

    int selectedCarIndex = 0;

    //Other components
    CarUIHandler carUIHandler = null;

    // Start is called before the first frame update
    void Start()
    {
        //Load the car data
        carDatas = Resources.LoadAll<CarData>("CarData/");

        StartCoroutine(SpawnCarCO(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnPreviousCar();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnNextCar();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectCar();
        }
    }

    public void OnPreviousCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex--;

        if (selectedCarIndex < 0)
            selectedCarIndex = carDatas.Length - 1;

        StartCoroutine(SpawnCarCO(true));
    }

    public void OnNextCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex++;

        if (selectedCarIndex > carDatas.Length - 1)
            selectedCarIndex = 0;

        StartCoroutine(SpawnCarCO(false));
    }

    public void OnSelectCar()
    {
        PlayerPrefs.SetInt("P1SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        //Multiple player input selection
        //PlayerPrefs.SetInt("P2SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        //PlayerPrefs.SetInt("P3SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        //PlayerPrefs.SetInt("P4SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);

        PlayerPrefs.Save();

        SceneManager.LoadScene(SelectMapUIHandler.mapToLoad);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        isChangingCar = true;

        if (carUIHandler != null)
            carUIHandler.StartCarExitAnimation(!isCarAppearingOnRightSide);

        GameObject instantiatedCar = Instantiate(carPrefab, spawnOnTransform);

        /*
        CarDisplay display = GetComponent<CarDisplay>();
        display.DisplayCar(carDatas[selectedCarIndex]);
        */

        DisplayCar(carDatas[selectedCarIndex]);

        carUIHandler = instantiatedCar.GetComponent<CarUIHandler>();
        carUIHandler.SetupCar(carDatas[selectedCarIndex]);
        carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.4f);

        isChangingCar = false;
    }

    public void DisplayCar(CarData cardata)
    {
        carName.text = cardata.carName;
        TopDownCarController stats = cardata.CarPrefab.GetComponent<TopDownCarController>();
        speed.fillAmount = stats.maxSpeed / 25;
        acc.fillAmount = stats.accelerationFactor / 30;
        handling.fillAmount = stats.turnFactor / 5;
    }
}
