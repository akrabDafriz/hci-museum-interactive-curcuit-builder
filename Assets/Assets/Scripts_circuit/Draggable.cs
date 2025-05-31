using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class untuk assets yang akan di-drag
public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition); 
        //menyimpan offset antara mouse & objek
    }

    void OnMouseDrag()
    {
        //update posisi objek berdasarkan pergerakan mouse
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z);
        Vector3 curPosition = cam.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        // Jika tidak menyentuh slot, balikin ke posisi awal
    }

}
