using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] int collisions = 3 ;
    [SerializeField] RCC_CarControllerV3 carController;
    [SerializeField] TextMeshProUGUI collsionsText;

    private void Start()
    {
        collsionsText.text = collisions.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (carController.enabled)
        {
            collisions--;
            collsionsText.text = collisions.ToString();
            if (collisions<=0)
            {
                collisions = 0;
                //GameOver
                Debug.Log("GameOver");
            }
        }
    }
}
