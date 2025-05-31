using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class untuk komponen seperti gates, LED, button, dll
public class CircuitComponent : MonoBehaviour
{
    public string componentName;

    [Header("Input/Output Points")]
    public Transform[] inputPoints;
    public Transform[] outputPoints;

    [Header("States")]
    public bool[] inputStates;
    public bool[] outputStates;

    public virtual void EvaluateLogic()
    {
        // Override di subclass nanti
    }
}
