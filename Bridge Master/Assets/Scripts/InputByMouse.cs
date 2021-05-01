using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputByMouse : MonoBehaviour
{
    [SerializeField] GirderMovements girderMovements;

    void Update()
    {
        GirderDropping();
    }
    void GirderDropping() // Kirişin tap timing e göre düşmesini ve makinenin eski pozisyonuna dönmesini sağlar
    {
        if (Input.GetMouseButtonDown(0))
        {
            girderMovements.successedPlayer = false;
            if(Mathf.Abs(transform.GetChild(1).transform.localPosition.x) <= 1f)
            {
                girderMovements.successedPlayer = true;
            }
            girderMovements.TimingControl();
        }
    }
}
