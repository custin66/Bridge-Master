using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentGirderController : MonoBehaviour
{
    [SerializeField] GirderStockController girderStockController;
    [SerializeField] GirderMovements girderMovements;
   // [SerializeField] Material RedMat, GreenMat;

    [SerializeField] GameObject TransparentGirder;

     private Material TransparentMat;
     private Material TransparentMatUst;
    void Awake()
    {
        TransparentMat = TransparentGirder.GetComponent<MeshRenderer>().material;
        TransparentMatUst = TransparentGirder.transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        ColorChanging();
    }
    public void MoveTransparentGirder()
    {
        TransparentGirder.transform.localPosition = new Vector3(0f, -5.7f, girderMovements.girderLocation);
    }

    void ColorChanging()
    {
        if (Mathf.Abs(transform.GetChild(1).transform.localPosition.x) <= 1f)
        {
            TransparentMat.color = Color.green;
            TransparentMatUst.color = Color.green;
        }
        else
        {
            TransparentMat.color = Color.red;
            TransparentMatUst.color = Color.red;
        }
    }


}
