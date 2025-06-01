using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableComponent : MonoBehaviour
{
    private Camera cam;
    private float CameraZDistance;

    private LEDController ledController;
    private BreadboardPinManager pinManager;

    private MonoBehaviour cameraFollowScript;
    private float originalFOV;
    private float zoomedFOV = 30f; // Smaller = more zoom
    private float zoomSpeed = 5f;
    private bool isDragging = false;


    void Start()
    {
        cam = Camera.main;
        CameraZDistance = cam.WorldToScreenPoint(transform.position).z;
        ledController = GetComponent<LEDController>();
        pinManager = FindObjectOfType<BreadboardPinManager>();
        originalFOV = Camera.main.fieldOfView;
    }

    void OnMouseDown()
    {
        // Temukan CameraFollow (Script) secara otomatis
        var mainCam = Camera.main;
        if (mainCam != null && cameraFollowScript == null)
        {
            cameraFollowScript = mainCam.GetComponent<MonoBehaviour>(); // fallback default
            foreach (var comp in mainCam.GetComponents<MonoBehaviour>())
            {
                if (comp.GetType().Name == "CameraFollow")
                {
                    cameraFollowScript = comp;
                    break;
                }
            }
        }

        if (cameraFollowScript != null)
            cameraFollowScript.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isDragging = true;
        var follow = Camera.main.GetComponent("CameraFollow") as MonoBehaviour;
        if (follow != null) follow.enabled = false;
    }

    void OnMouseDrag()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
        Vector3 newWorldPosition = cam.ScreenToWorldPoint(screenPosition);
        transform.position = newWorldPosition;
    }

    void OnMouseUp()
    {
        if (cameraFollowScript != null)
            cameraFollowScript.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Snapping logic
        float maxSnapDistance = 0.05f;
        BreadboardPin nearestPin = null;
        float nearestDist = Mathf.Infinity;

        foreach (var pin in FindObjectsOfType<BreadboardPin>())
        {
            float dist = Vector3.Distance(transform.position, pin.transform.position);
            if (dist < nearestDist && dist < maxSnapDistance)
            {
                nearestPin = pin;
                nearestDist = dist;
            }
        }

        if (nearestPin != null)
        {
            transform.position = nearestPin.transform.position;
            nearestPin.ConnectComponent(this);
            Debug.Log($"Component snapped to: {nearestPin.name}");
        }
        isDragging = false;
        // Re-enable camera follow
        var follow = Camera.main.GetComponent("CameraFollow") as MonoBehaviour;
        if (follow != null) follow.enabled = true;
    }
    void Update()
    {
        float targetFOV = isDragging ? zoomedFOV : originalFOV;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
