using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUIHandler : MonoBehaviour
{
    [Header("Map details")]
    public Image mapImage;
   
    Animator animator = null;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {

    }

    public void SetupMap(MapData mapData)
    {
        mapImage.sprite = mapData.MapImage;

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


    public void OnMapExitAnimationCompleted()
    {
        Destroy(gameObject);
    }
}
