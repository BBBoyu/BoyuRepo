using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New AI Car", menuName = "AI Car", order = 51)]

public class AICar : ScriptableObject
{
    public int CarUniqueID;
    public Sprite CarUISprite;
    public GameObject CarPrefab;
    public string carName;
    public string carDescription;
}
