using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandlerWithDelay : MonoBehaviour
{
    TopDownCarController CarController;

    //Awake as soon as loaded
    
    void Awake()
    {
        //CarController = GetComponent<TopDownCarController>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        CarController = GetComponent<TopDownCarController>();
    }

    /*
    void Start()
    {
        CarController = GetComponent<TopDownCarController>();

    }
    */

    // Updated every frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        //Get input from Unity's input system.
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        //Send the input to the car controller.
        CarController.SetInputVector(inputVector);
    }
}
