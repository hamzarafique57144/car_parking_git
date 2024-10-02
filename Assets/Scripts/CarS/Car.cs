using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] CarData carData;
    public float timer = 30f;
    public bool startCountDown = false;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.GetTimerText.text = timer.ToString("F0");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(this.gameObject.GetComponent<RCC_CarControllerV3>().enabled)
        {
            gameManager.OnCarHit();
        }
       
    }
    public IEnumerator StartCountDown()
    {
        yield return null;
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            CarSelector.Instance.SwitchControlToPlayer();
        }
        if (startCountDown)
        {
            StartCoroutine(StartCountDown());
        }
    }
    public CarData GetCarData() { return carData; }
}
