﻿using UnityEngine;

[CreateAssetMenu (fileName ="New Map", menuName ="Map")]
public class Map : ScriptableObject
{
    public int mapIndex;
    public string mapName;
    public string mapDescription;
    //public Color nameColor;
    public Sprite mapImage;
    public string sceneToLoad;
}