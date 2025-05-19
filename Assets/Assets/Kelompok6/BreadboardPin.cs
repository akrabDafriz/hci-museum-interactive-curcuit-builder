using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadboardPin : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} clicked!");
    }
}