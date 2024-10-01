using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] CarData carData;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(this.gameObject.GetComponent<RCC_CarControllerV3>().enabled)
        {
            GameManager.Instance.OnCarHit();
        }
       
    }

    public CarData GetCarData() { return carData; }
}
