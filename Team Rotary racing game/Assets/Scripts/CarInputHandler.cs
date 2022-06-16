using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    TopDownCarController CarController;
    public int playerNumber = 1;

    //Awake as soon as loaded

    void Awake()
    {
        //CarController = GetComponent<TopDownCarController>();
    }
    

    void Start()
    {
        CarController = GetComponent<TopDownCarController>();
    }

    // Updated every frame
    void Update()
    {

        if (GameController__update.instance.gamePlaying)
        {
            Vector2 inputVector = Vector2.zero;

            //Get input from Unity's input system.
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            //Send the input to the car controller.
            CarController.SetInputVector(inputVector);
        }
        else
        {
            Vector2 inputVector = Vector2.zero;
            CarController.SetInputVector(inputVector);

        }
    }
}
