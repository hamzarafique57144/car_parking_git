using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingController : MonoBehaviour
{
    public void DriveThisCar()
    {
        Debug.Log("Drive " + gameObject.name);
    }
}
