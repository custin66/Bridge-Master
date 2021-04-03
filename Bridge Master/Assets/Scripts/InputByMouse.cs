using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputByMouse : MonoBehaviour
{
    GirderMovements girderMovements;

    private void Awake()
    {
        girderMovements = FindObjectOfType<GirderMovements>();
    }
    void Update()
    {
        GirderDropping();
    }
    void GirderDropping() // Kirişin tap timing e göre düşmesini ve makinenin eski pozisyonuna dönmesini sağlar
    {
        if (Input.GetMouseButtonDown(0))
        {
            girderMovements.TimingControl();
        }
    }
}
