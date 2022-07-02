using UnityEngine;

[CreateAssetMenu (fileName ="New Map", menuName ="Map")]
public class MapData : ScriptableObject
{
    /*
    public string mapName;
    public string mapDescription;
    */

    public string sceneToLoad;
    public Sprite MapImage;
    public int MapIndex;

    /*

    [SerializeField]
    private int mapIndex = 0;

    [SerializeField]
    private Sprite mapImage;

    public int MapIndex
    {
        get { return mapIndex; }
    }
    public Sprite MapImage
    {
        get { return mapImage; }
    }
    */


}