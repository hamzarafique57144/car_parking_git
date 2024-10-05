using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
        hitCanvas.SetActive(false);
        notEnoughMoneytoSwitchCarCanvas.enabled = false;
    }

    private void Update()
    {
        SelectedCarSlider();


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
        }
       
    }
    
    private void DisbaleCarHitPanel()
    {
        hitCanvas.SetActive(false);
    }
    public bool CarSelectionCameraActive()
    {
        return carSelectionCamera.enabled;
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
