using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    private RCC_CarControllerV3 playerCar;
    public Camera mainCamera; // Assign your main camera in the Inspector
    private RCC_CarControllerV3 selectedCar;
    private Rigidbody playerCarRigidbody;

    private void Start()
    {
        playerCar = GameObject.FindGameObjectWithTag(Tags.PlayerCarTag).GetComponent<RCC_CarControllerV3>();
        playerCarRigidbody = playerCar.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0)) // For touch, you could use Input.touchCount > 0 and Input.GetTouch(0).phase == TouchPhase.Began
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction, UnityEngine.Color.red);
                Debug.Log(hit.transform.name);
                Debug.Log("Raycasting");
                RCC_CarControllerV3 car = hit.transform.GetComponent<RCC_CarControllerV3>();

                if (car != null && hit.collider.tag!="Player")
                {
                    selectedCar = car;
                    if (selectedCar != null)
                    {
                        playerCar.enabled = false;
                        playerCarRigidbody.isKinematic = true;
                        selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        selectedCar.enabled = true;
                        Invoke(nameof(SwitchControlToPlayer), 5f);
                       // selectedCar.SetControlled(false); // Deselect current car
                    }

                   
                   // car.SetControlled(true); // Select new car
                }
            }
        }
    }

    void SwitchControlToPlayer()
    {
        playerCar.enabled = true;
        playerCarRigidbody.isKinematic = false;
        selectedCar.enabled = false;
        selectedCar.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
