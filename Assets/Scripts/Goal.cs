using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == Tags.PlayerCarTag)
        {
            //Level Completed
            Debug.Log("Level Completed");
            GameManager.Instance.LevelComplete();
        }
        else if (other.gameObject.tag == Tags.CarTag)
        {
            Debug.Log("Game over");
            GameManager.Instance.GameOver();
        }
    }
   
}
