using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject currentComponent = null;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && other.CompareTag("Component"))
        {
            currentComponent = other.gameObject;
            isOccupied = true;

            // Snap position
            other.transform.position = transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentComponent)
        {
            currentComponent = null;
            isOccupied = false;
        }
    }
}