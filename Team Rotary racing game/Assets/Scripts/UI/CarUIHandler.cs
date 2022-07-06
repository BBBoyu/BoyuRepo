using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUIHandler : MonoBehaviour
{
    [Header("Car details")]
    public Image carImage;
    public Text carName;

    [Header("Car stats")]
    public Image carSpeed;

    Animator animator = null;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

    }

    void Start()
    {

    }

    public void SetupCar(CarData carData)
    {
        carImage.sprite = carData.CarUISprite;

    }

    public void StartCarEntranceAnimation(bool isAppearingOnRightSide)
    {
        if (isAppearingOnRightSide)
            animator.Play("CarUI from Right");
        else animator.Play("CarUI from Left");
    }

    public void StartCarExitAnimation(bool isExitingOnRightSide)
    {
        if (isExitingOnRightSide)
            animator.Play("CarUI to Right");
        else animator.Play("CarUI to Left");
    }

    //Events
    public void OnCarExitAnimationCompleted()
    {
        Destroy(gameObject);
    }
}
