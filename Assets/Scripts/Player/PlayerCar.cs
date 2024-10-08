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
            GameManager.Instance.collisions +=1;
            collsionsText.text = collisions.ToString();
            if (collisions<=0)
            {
                collisions = 0;
                GameManager.Instance.GameOver();
                Debug.Log("GameOver");
            }
        }
    }
}
