using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    TopDownCarController CarController;
    public int playerNumber = 1;

    void Awake()
    {
    }
    

    void Start()
    {
        CarController = GetComponent<TopDownCarController>();
    }

    void Update()
    {

        if (GameController__update.instance.gamePlaying)
        {
            Vector2 inputVector = Vector2.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            CarController.SetInputVector(inputVector);
        }
        else
        {
            Vector2 inputVector = Vector2.zero;
            CarController.SetInputVector(inputVector);

        }
    }
}
