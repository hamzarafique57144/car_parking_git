using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    private RCC_CarControllerV3 playerCar;
    public Camera mainCamera; // Assign your main camera in the Inspector
    private RCC_CarControllerV3 selectedCar;
    private Rigidbody playerCarRigidbody;
    [SerializeField] GameObject Camera_RCC;
    [SerializeField] Camera CarSelectionCamera;
    public LayerMask carLayerMask;
    bool canSwitch = false;

    private void Start()
    {
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
                    selectedCar = car;
                    if (selectedCar != null)
                    {
                        //We can switch control from a selected to other
                        canSwitch = true;
                        playerCar.enabled = false;
                        playerCarRigidbody.isKinematic = true;
                        selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        selectedCar.enabled = true;
                        Camera_RCC.SetActive(true);
                        CarSelectionCamera.gameObject.SetActive(false);
                        Invoke(nameof(SwitchControlToPlayer), 30f);
                        // selectedCar.SetControlled(false); // Deselect current car
                    }


                    // car.SetControlled(true); // Select new car
                }
            }
        }
    }
    void SwitchControlToPlayer()
    {
        //We can not switch car untill we press switch button
        canSwitch = false;
        playerCar.enabled = true;
        playerCarRigidbody.isKinematic = false;
        selectedCar.enabled = false;
        selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnSkipButtonClick()
    {
        //Switch controll from player to other car
        canSwitch = true;
        playerCar.enabled = false;
        playerCarRigidbody.isKinematic = true;
        //When we want to switch control from a selected car to other
        if(selectedCar!=null)
        {
            selectedCar.enabled = false;
            selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        /*Vector3 RccCameraPosition = Camera_RCC.transform.position;
        CarSelectionCamera.gameObject.transform.position = new Vector3(RccCameraPosition.x, CarSelectionCamera.transform.position.y, RccCameraPosition.z);
        */
        Camera_RCC.SetActive(false);
        CarSelectionCamera.gameObject.SetActive(true);
       
    }
    public  Car GetCar()
    {
        Car car = selectedCar.gameObject.GetComponent<Car>();
        return car;
    }
}
