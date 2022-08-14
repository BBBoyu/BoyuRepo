using UnityEngine;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour
{
    public Text carName;

    public void DisplayCar(CarData cardata)
    {
        carName.text = cardata.carName;
    }
}