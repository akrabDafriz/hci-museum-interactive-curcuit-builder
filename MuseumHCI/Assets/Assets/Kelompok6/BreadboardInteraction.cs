using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadboardInteraction : MonoBehaviour
{
    public Transform assemblyCameraPosition;
    public float transitionSpeed = 2f;
    private Transform camTransform;
    private MonoBehaviour cameraFollowScript;

    private bool playerInRange = false;
    private bool inAssemblyMode = false;

    void Start()
    {
        camTransform = Camera.main.transform;
        cameraFollowScript = Camera.main.GetComponent("CameraFollow") as MonoBehaviour;
    }

    void Update()
    {
        // Player presses "E" while in range
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !inAssemblyMode)
        {
            EnterAssemblyMode();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press 'E' to interact with the breadboard.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void EnterAssemblyMode()
    {
        inAssemblyMode = true;

        if (cameraFollowScript != null)
            cameraFollowScript.enabled = false;

        StartCoroutine(MoveCameraToAssemblyView());
    }

    IEnumerator MoveCameraToAssemblyView()
    {
        while (Vector3.Distance(camTransform.position, assemblyCameraPosition.position) > 0.01f)
        {
            camTransform.position = Vector3.Lerp(camTransform.position, assemblyCameraPosition.position, Time.deltaTime * transitionSpeed);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, assemblyCameraPosition.rotation, Time.deltaTime * transitionSpeed);
            yield return null;
        }

        Debug.Log("Assembly mode active.");
    }
}