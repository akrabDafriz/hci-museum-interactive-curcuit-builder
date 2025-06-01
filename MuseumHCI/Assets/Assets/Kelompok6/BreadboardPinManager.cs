using System.Collections.Generic;
using UnityEngine;

public class BreadboardPinManager : MonoBehaviour
{
    public GameObject pinPrefab;
    public Vector3 localStartPos = new Vector3(0, 0.1f, 0);
    public int rows = 2;
    public int columns = 5;
    public float spacingX = 0.068f;
    public float spacingZ = 0.068f;

    // Menyimpan semua pin
    public List<BreadboardPin> allPins = new List<BreadboardPin>();

    // Menyimpan koneksi antar pin dalam kolom
    private Dictionary<int, List<BreadboardPin>> columnPinMap = new Dictionary<int, List<BreadboardPin>>();

    // Menyimpan semua LED
    public List<LEDController> ledControllers = new List<LEDController>();

    void Start()
    {
        GeneratePins();
    }

    void GeneratePins()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 localPos = localStartPos + new Vector3(col * spacingX, 0, row * spacingZ);
                Vector3 worldPos = transform.parent.TransformPoint(localPos);
                GameObject pinObj = Instantiate(pinPrefab, worldPos, Quaternion.identity, this.transform);
                pinObj.name = $"Pin_{row}_{col}";

                BreadboardPin pin = pinObj.GetComponent<BreadboardPin>();
                pin.row = row;
                pin.column = col;
                pin.manager = this;

                // Simpan ke daftar pin
                allPins.Add(pin);

                // Simpan ke kolom
                if (!columnPinMap.ContainsKey(col))
                    columnPinMap[col] = new List<BreadboardPin>();
                columnPinMap[col].Add(pin);
            }
        }
    }

    // Ambil semua pin dalam satu kolom (untuk logika sambungan)
    public List<BreadboardPin> GetConnectedPins(int column)
    {
        return columnPinMap.ContainsKey(column) ? columnPinMap[column] : new List<BreadboardPin>();
    }

    // Update semua LED saat ada perubahan pin
    public void UpdateAllLEDs()
    {
        foreach (var led in ledControllers)
        {
            if (led != null)
                led.UpdateLED();
        }
    }

    // Fungsi bantu: cari pin terdekat untuk snap
    public BreadboardPin GetNearestPin(Vector3 position, float maxDistance)
    {
        BreadboardPin nearest = null;
        float closestDist = Mathf.Infinity;

        foreach (BreadboardPin pin in allPins)
        {
            float dist = Vector3.Distance(position, pin.transform.position);
            if (dist < closestDist && dist <= maxDistance)
            {
                closestDist = dist;
                nearest = pin;
            }
        }

        return nearest;
    }
}
