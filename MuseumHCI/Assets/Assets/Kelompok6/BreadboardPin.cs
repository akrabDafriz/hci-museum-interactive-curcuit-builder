using System.Collections.Generic;
using UnityEngine;

public class BreadboardPin : MonoBehaviour
{
    [Header("Info Pin")]
    public int row;
    public int column;
    public BreadboardPinManager manager;

    [Header("Status")]
    public bool isPowered = false;

    // Komponen yang sedang tersambung ke pin ini
    public List<MonoBehaviour> connectedComponents = new List<MonoBehaviour>();

    /// Fungsi untuk toggle power pin ini (simulasi manual)
    void OnMouseDown()
    {
        isPowered = !isPowered;
        Debug.Log($"{gameObject.name} powered: {isPowered}");

        // Update semua LED saat ada pin berubah status
        manager?.UpdateAllLEDs();
    }

    /// Komponen (misalnya LED) bisa daftar diri saat menempel
    public void ConnectComponent(MonoBehaviour component)
    {
        if (!connectedComponents.Contains(component))
        {
            connectedComponents.Add(component);
            Debug.Log($"{component.name} connected to {gameObject.name}");
        }
    }

    /// Komponen bisa dilepas dari pin ini
    public void DisconnectComponent(MonoBehaviour component)
    {
        if (connectedComponents.Contains(component))
        {
            connectedComponents.Remove(component);
            Debug.Log($"{component.name} disconnected from {gameObject.name}");
        }
    }

    /// Apakah pin ini "aktif" secara logika (contohnya, jika ada pin lain di kolom ini yang bertenaga)
    public bool IsLogicallyPowered()
    {
        if (isPowered) return true;

        if (manager == null) return false;

        List<BreadboardPin> connectedPins = manager.GetConnectedPins(column);
        foreach (var pin in connectedPins)
        {
            if (pin != this && pin.isPowered)
                return true;
        }

        return false;
    }
}
