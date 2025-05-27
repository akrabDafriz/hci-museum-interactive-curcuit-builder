using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadboardPinManager : MonoBehaviour
{
    public GameObject pinPrefab;
    public Vector3 localStartPos = new Vector3(0, 0.1f, 0); // relatif terhadap breadboard
    public int rows = 2;
    public int columns = 5;
    public float spacingX = 0.000305f;
    public float spacingZ = 0.002455f;

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
                Vector3 worldPos = transform.parent.TransformPoint(localPos); // ubah ke world space
                GameObject pin = Instantiate(pinPrefab, worldPos, Quaternion.identity, this.transform);
                pin.name = $"Pin_{row}_{col}";
            }
        }
    }
}
