using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] Camera carSelectionCamera;
    Vector2 dragDelta;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // Rotate the camera horizontally (Y-axis)
        float horizontalRotation = dragDelta.x * rotationSpeed * Time.deltaTime;
        carSelectionCamera.transform.Rotate(Vector3.up, -horizontalRotation, Space.World);

        // Rotate the camera vertically (X-axis)
        float verticalRotation = dragDelta.y * rotationSpeed * Time.deltaTime;
        carSelectionCamera.transform.Rotate(Vector3.right, verticalRotation, Space.Self);
    }
    public void OnDrag(PointerEventData data)
    {
        // Calculate the difference in drag position (data.delta)
         dragDelta = data.delta;

        


    }

    public bool CarSelectionCameraActive()
    {
        return carSelectionCamera.enabled;
    }
}
