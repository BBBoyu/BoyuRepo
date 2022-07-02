using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Car Data", menuName = "Car Data", order = 51)]

public class CarData : ScriptableObject
{
    public int CarUniqueID;
    public Sprite CarUISprite;
    public GameObject CarPrefab;
    public string carName;
    /*
    public string carSpeed;
    public string carAcc;
    public string carHandle;
    */
    /*
    [SerializeField]
    private int carUniqueID = 0;

    [SerializeField]
    private Sprite carUISprite;

    [SerializeField]
    private GameObject carPrefab;

    [SerializeField]
    private string carName;

    public string CarName
    {
        get { return carName; }
    }

    public int CarUniqueID
    {
        get { return carUniqueID; }
    }
    public Sprite CarUISprite
    {
        get { return carUISprite; }
    }
    public GameObject CarPrefabbb
    {
        get { return carPrefab; }
    }
    */

}
