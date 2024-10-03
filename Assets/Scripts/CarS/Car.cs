using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] CarData carData;
    public float timer = 0f;
    GameManager gameManager;
    public bool isSelected;
    private void Start()
    {
        gameManager = GameManager.Instance;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(CheckCarIsSelected())
        {
            gameManager.OnCarHit();
        }
       
    }
   
    public bool CheckCarIsSelected()
    {
        return this.gameObject.GetComponent<RCC_CarControllerV3>().enabled;
            
    }
    public CarData GetCarData() { return carData; }
}
