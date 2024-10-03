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
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hitCanvas.SetActive(false);
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
        hitCanvas.SetActive(true);
        CarData carData = carSelector.GetCar().GetCarData();
        carNameText.text = carData.GetCarName;
        hitText.text =  carData.GetCarPrice+" coins are deducted from your account";
        Invoke(nameof(DisbaleCarHitPanel), 3.5f);
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
