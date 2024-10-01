using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create Data",fileName = "Car Data")]
public class CarData : ScriptableObject
{
    public string carName;
    public int price;


    public string GetCarName
    {
        get { return carName; }
    }

    public int GetCarPrice
    {
        get { return price; }
    }
    
}
