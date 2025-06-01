using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDController : MonoBehaviour
{
    [Header("Pin Referensi")]
    public BreadboardPin positivePin;
    public BreadboardPin negativePin;

    [Header("Visual")]
    public Renderer ledRenderer;
    public Color offColor = Color.black;
    public Color onColor = Color.red;

    private void Start()
    {
        UpdateLED();
    }


    /// Update status LED berdasarkan koneksi ke pin
    public void UpdateLED()
    {
        if (positivePin == null || negativePin == null)
        {
            SetLED(false);
            return;
        }

        // Cek apakah arus logis bisa mengalir dari positive ke negative
        bool isPowered = positivePin.IsLogicallyPowered() && !negativePin.IsLogicallyPowered();

        SetLED(isPowered);
    }


    /// Ubah tampilan LED (nyala atau tidak)
    private void SetLED(bool state)
    {
        if (ledRenderer != null)
        {
            ledRenderer.material.color = state ? onColor : offColor;
        }
    }


    /// Pasangkan LED ke dua pin terdekat
    public void TryAutoConnectPins(BreadboardPinManager manager, float maxDistance = 0.05f)
    {
        if (manager == null) return;

        BreadboardPin first = null;
        BreadboardPin second = null;
        float bestDist = Mathf.Infinity;

        foreach (var pinA in manager.allPins)
        {
            foreach (var pinB in manager.allPins)
            {
                if (pinA == pinB) continue;

                float distA = Vector3.Distance(transform.position, pinA.transform.position);
                float distB = Vector3.Distance(transform.position, pinB.transform.position);
                float total = distA + distB;

                if (distA < maxDistance && distB < maxDistance && total < bestDist)
                {
                    bestDist = total;
                    first = pinA;
                    second = pinB;
                }
            }
        }

        if (first != null && second != null)
        {
            positivePin = first;
            negativePin = second;
            first.ConnectComponent(this);
            second.ConnectComponent(this);
            Debug.Log($"LED connected to: {first.name} and {second.name}");
        }

        UpdateLED();
    }
}
