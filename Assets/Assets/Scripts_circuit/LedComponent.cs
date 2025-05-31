using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedComponent : CircuitComponent
{
    [Header("Visuals")]
    public GameObject ledMesh; // assign the visible part of LED in Inspector
    public Material onMaterial;
    public Material offMaterial;

    private Renderer ledRenderer;

    void Start()
    {
        // Get the renderer from the mesh
        if (ledMesh != null)
        {
            ledRenderer = ledMesh.GetComponent<Renderer>();
        }

        EvaluateLogic(); // initial state
    }

    public override void EvaluateLogic()
    {
        // Check input state (assume 1 input)
        bool isOn = inputStates.Length > 0 && inputStates[0];

        if (ledRenderer != null)
        {
            // Change material based on input
            ledRenderer.material = isOn ? onMaterial : offMaterial;
        }

        // Optionally: update outputStates[0] = isOn;
        if (outputStates.Length > 0)
        {
            outputStates[0] = isOn;
        }
    }
}
