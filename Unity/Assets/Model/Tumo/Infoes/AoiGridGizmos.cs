using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiGridGizmos : MonoBehaviour
{
    public int count = 5;
    public int wide = 20;
    public int mapLength = 100;
    public bool isLine = true;

    Vector3 xf0 { get; set; }
    Vector3 xf1 { get; set; }
    Vector3 yf0 { get; set; }
    Vector3 yf1 { get; set; }
    Vector3 offset { get; set; }

    private void Start()
    {
        offset = new Vector3(mapLength / 2, 0, mapLength / 2);
    }

    void Update()
    {
        if (!isLine) return;
        for (int x = 0; x <= 5; x++)
        {
            for (int y = 0; y <= 5; y++)
            {
                xf0 = new Vector3(0, 0, y * wide) - offset;
                xf1 = new Vector3(mapLength, 0, y * wide) - offset;
                yf0 = new Vector3(x * wide, 0, 0) - offset;
                yf1 = new Vector3(x * wide, 0, mapLength) - offset;
                Debug.DrawLine(xf0, xf1, Color.white, 1.0f, false);
                Debug.DrawLine(yf0, yf1, Color.white, 1.0f, false);
            }
        }

    }

}
