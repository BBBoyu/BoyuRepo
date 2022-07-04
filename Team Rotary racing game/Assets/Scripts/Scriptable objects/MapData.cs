using UnityEngine;

[CreateAssetMenu (fileName ="New Map", menuName ="Map")]
public class MapData : ScriptableObject
{
    
    
    public string sceneToLoad;
    public Sprite MapImage;
    public int MapIndex;
    public string mapName;
    public string mapDescription;

}