using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUIHandler : MonoBehaviour
{
    [Header("Map details")]
    public Image mapImage;

    //Other components
    Animator animator = null;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetupMap(MapData mapData)
    {
        //Debug.Log(mapData.MapIndex);
        mapImage.sprite = mapData.MapImage;
        //mapImage.sprite = mapData.MapImage;
    }

    public void StartMapEntranceAnimation(bool isAppearingOnRightSide)
    {
        if (isAppearingOnRightSide)
            animator.Play("MapUI from right");
        else animator.Play("MapUI from left");
    }

    public void StartMapExitAnimation(bool isExitingOnRightSide)
    {
        if (isExitingOnRightSide)
            animator.Play("MapUI to right");
        else animator.Play("MapUI to left");
    }

    //Events
    public void OnMapExitAnimationCompleted()
    {
        Destroy(gameObject);
    }
}
