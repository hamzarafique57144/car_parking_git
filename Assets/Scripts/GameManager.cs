using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] Camera carSelectionCamera;

    [SerializeField] CarSelector carSelector;
    [SerializeField] GameObject hitCanvas;
    [SerializeField] TextMeshProUGUI hitText;
    [SerializeField] TextMeshProUGUI carNameText;

    [SerializeField] GameObject playerCarCollisionPanel;
    [SerializeField] GameObject selectedCarTimerPanel;

    [SerializeField] Slider carSlider;
    private float carTimer = 30;

    public Canvas notEnoughMoneytoSwitchCarCanvas;
    public Canvas RccCanvas;
   
    bool levelCompleted;
    int trackTime;

    [Header ("Level Complete Things")]
    [SerializeField] Canvas levelCompletedCanvas;
    [SerializeField] TextMeshProUGUI levelCompleteCollisionTxt;
    [SerializeField] TextMeshProUGUI levelCompleteTimeTxt;
    [SerializeField] TextMeshProUGUI levelCompleteCashTxt;

    [Header("Game Over Things")]
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] TextMeshProUGUI gameOverCollisionTxt;
    [SerializeField] TextMeshProUGUI gameOverTimeTxt;
    [SerializeField] TextMeshProUGUI gameOverCashTxt;
    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey(PLAYER_PREFS.Cash))
        {
            CashManager.SetCashData(CashManager.defualtCash);
        }

    }

    private void Start()
    {
        trackTime = 0;
        hitCanvas.SetActive(false);
        notEnoughMoneytoSwitchCarCanvas.enabled = false;
        Invoke(nameof(TrackTheTime), 1f);
    }

    private void Update()
    {
        SelectedCarSlider();
        
        
    }

    private void TrackTheTime()
    {
        trackTime += 1;
        if (!levelCompleted)
        {
             Invoke(nameof(TrackTheTime), 1f);
            Debug.Log("Time is " + trackTime);
        }
    }
    private void SelectedCarSlider()
    {
        if (carSelector.isCarSelected)
        {

            carSlider.value += Time.deltaTime / carTimer;
            if (carSlider.value == 1)
            {
                carSelector.SwitchControlToPlayer();
            }
        }
        else
        {
            carSlider.value = 0;
        }
    }
    public void OnCarHit()
    {
        
        CarData carData = carSelector.GetCar().GetCarData();
        if (!carData.GetSelectionStatus())
        {
            hitCanvas.SetActive(true);
            carNameText.text = carData.GetCarName;
            hitText.text = carData.GetCarPrice + " coins are deducted from your account";
            CashManager.DeductCash(carData.GetCarPrice);
            Invoke(nameof(DisbaleCarHitPanel), 3.5f);
            Debug.Log("Cash is " + CashManager.GetSavedCash());
        }
        else
        {
            //GameOver
            CashManager.ClearCash();
            Debug.Log("GameOver");
            GameOver();
        }
       
    }
    
    public void LevelComplete()
    {
        /* RccCanvas.enabled = false;
         levelCompletedCanvas.enabled = true;*/
        levelCompleted = true;
        Time.timeScale = 0f;
        levelCompletedCanvas.enabled = true;
        RccCanvas.enabled = false;
        CashManager.AddCash(10000);
        levelCompleteCashTxt.text = CashManager.GetSavedCash().ToString();
        levelCompleteTimeTxt.text = GetTime();
        Debug.Log("Taken Time is " + GetTime());
    }
    public void GameOver()
    {
        levelCompleted = true;
        Time.timeScale = 0f;
        gameOverCanvas.enabled = true;
        RccCanvas.enabled = false;
        gameOverCashTxt.text = CashManager.GetSavedCash().ToString();
        gameOverTimeTxt.text = GetTime();
        Debug.Log("Taken Time is " + GetTime());
    }
    private void DisbaleCarHitPanel()
    {
        hitCanvas.SetActive(false);
    }
    public bool CarSelectionCameraActive()
    {
        return carSelectionCamera.enabled;
    }
    public string GetTime()
    {
        
        // Calculate hours
        int hours = Mathf.FloorToInt(trackTime / 3600);
        trackTime %= 3600;

        // Calculate minutes
        int minutes = Mathf.FloorToInt(trackTime / 60);
        trackTime %= 60;

        // Remaining seconds
        int seconds = Mathf.FloorToInt(trackTime);
        string totalTime = hours+":"+minutes+":"+seconds;
        
        return totalTime;
    }
    #region CarTimerPanel
    public void EnableTimerPanel()
    {
        DisablePlayerCarCollisionPanel();
        selectedCarTimerPanel.SetActive(true);
    }

    public void DisableTimerPanel()
    {
        playerCarCollisionPanel.SetActive(true);
        selectedCarTimerPanel.SetActive(false);
    }
     
    public void DisablePlayerCarCollisionPanel()
    {
        playerCarCollisionPanel.SetActive(false);
    }
    #endregion
}
