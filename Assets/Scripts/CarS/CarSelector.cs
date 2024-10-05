using System;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public static CarSelector Instance { get; private set; }
    private RCC_CarControllerV3 playerCar;
    public Camera mainCamera; // Assign your main camera in the Inspector
    private RCC_CarControllerV3 selectedCar;
    private Rigidbody playerCarRigidbody;
    [SerializeField] GameObject Camera_RCC;
    [SerializeField] Camera CarSelectionCamera;
    public LayerMask carLayerMask;
    //We can not switch car untill we press switch button
    bool canSwitch = false;
    public event EventHandler OnCarControllSwitch;
    Car selectedVehicle;
    public bool isCarSelected = false;
    private GameManager gameManager;
    [SerializeField] TextMeshProUGUI notEnoughMoneyDesciptioniText;
    private void Start()
    {
        gameManager = GameManager.Instance;
        playerCar = GameObject.FindGameObjectWithTag(Tags.PlayerCarTag).GetComponent<RCC_CarControllerV3>();
        playerCarRigidbody = playerCar.GetComponent<Rigidbody>();
    }
    void Update()
    {
       if(canSwitch)
        {
            SwitchControlFromPlayerToCar();
            
        }
    }

    void SwitchControlFromPlayerToCar()
    {
        if (Input.GetMouseButton(0)) // For touch, you could use Input.touchCount > 0 and Input.GetTouch(0).phase == TouchPhase.Began
        {
            Ray ray = CarSelectionCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, carLayerMask))
            {
                
                Debug.DrawRay(ray.origin, ray.direction, UnityEngine.Color.red);
                Debug.Log(hit.transform.name);
                Debug.Log("Raycasting");
                RCC_CarControllerV3 car = hit.transform.GetComponent<RCC_CarControllerV3>();
                
                if (car != null && hit.collider.tag != "Player")
                {
                    selectedVehicle = hit.transform.GetComponent<Car>();
                    if (CashManager.GetSavedCash() < selectedVehicle.GetCarData().GetCarPrice)
                    {
                        Time.timeScale = 0f;
                        //Further Logic
                        Debug.Log("You can not select this car");
                        notEnoughMoneyDesciptioniText.text = "Your current balance is " + CashManager.GetSavedCash().ToString() +
                            " and the damage cost for this car is " + selectedVehicle.GetCarData().GetCarPrice.ToString() +
                            " so you won't be able to pay fine.So avoid further accidents to stay in the game!";
                        gameManager.notEnoughMoneytoSwitchCarCanvas.enabled = true;
                        gameManager.RccCanvas.enabled = false;
                    }
                    selectedCar = car;
                        if (selectedCar != null)
                        {
                            isCarSelected = true;
                           
                            OnCarControllSwitch?.Invoke(this, EventArgs.Empty);
                            gameManager.EnableTimerPanel();
                            //We can switch control from a selected to other
                            canSwitch = true;
                            playerCar.KillEngine();
                            playerCar.enabled = false;
                            playerCarRigidbody.isKinematic = true;
                            selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                            selectedCar.enabled = true;
                            selectedCar.StartEngine();
                            Camera_RCC.SetActive(true);
                            CarSelectionCamera.gameObject.SetActive(false);
                        }
                   
                       // Invoke(nameof(SwitchControlToPlayer), 30f);
                        // selectedCar.SetControlled(false); // Deselect current car
                    

                    // car.SetControlled(true); // Select new car
                }
            }
        }
    }

    public void DangerousCarSelection()
    {
        //When we don't have enough coins to switch the car
        //but we decided to switch the car, it will be a
        //dangerous selection,in dangerous selection, if car hit,
        //the game will over
        Time.timeScale = 1f;
        selectedVehicle.GetCarData().SetDangerousSelection();
        gameManager.notEnoughMoneytoSwitchCarCanvas.enabled = false;
        gameManager.RccCanvas.enabled = true;
    }

    public void DriveOtherCar()
    {
        Time.timeScale = 1f;
        gameManager.notEnoughMoneytoSwitchCarCanvas.enabled = false;
        gameManager.RccCanvas.enabled = true;
        OnSkipButtonClick();
    }
    public void SwitchControlToPlayer()
    {
        selectedVehicle.GetCarData().SetSafeSelection();
        isCarSelected = false ;
        gameManager.DisableTimerPanel();
        //We can not switch car untill we press switch button
        canSwitch = false;
        playerCar.enabled = true;
        playerCar.StartEngine();
        playerCarRigidbody.isKinematic = false;
        selectedCar.KillEngine();
        selectedCar.enabled = false;
        
        selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    public void OnSkipButtonClick()
    {
        if(selectedVehicle!= null)
        {
            selectedVehicle.GetCarData().SetSafeSelection();
        } 
        isCarSelected = false;
        //Switch controll from player to other car
        canSwitch = true;
        playerCar.KillEngine();
        playerCar.enabled = false;
        playerCarRigidbody.isKinematic = true;
        //When we want to switch control from a selected car to other
        if(selectedCar!=null)
        {
            selectedCar.KillEngine();
            selectedCar.enabled = false;
            
            selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        /*Vector3 RccCameraPosition = Camera_RCC.transform.position;
        CarSelectionCamera.gameObject.transform.position = new Vector3(RccCameraPosition.x, CarSelectionCamera.transform.position.y, RccCameraPosition.z);
        */
        Camera_RCC.SetActive(false);
        CarSelectionCamera.gameObject.SetActive(true);
        gameManager.DisableTimerPanel();
        gameManager.DisablePlayerCarCollisionPanel();




    }
    public  Car GetCar()
    {
        
           return selectedVehicle;
        
    }
}
