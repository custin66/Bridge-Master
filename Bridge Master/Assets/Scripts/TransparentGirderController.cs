using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentGirderController : MonoBehaviour
{
    [SerializeField] GirderStockController girderStockController;
    [SerializeField] GirderMovements girderMovements;
    [SerializeField] Material RedMat, GreenMat;

    // [SerializeField] GameObject TransparentGirder;

    private Material TransparentMat;
    private Material TransparentMatUst;
    [SerializeField] Material GirderMat;

    [HideInInspector] public bool originalMaterial = false;
    void Start()
    {
        // GirderMat = transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material;

        //TransparentMat = TransparentGirder.GetComponent<MeshRenderer>().material;
        //TransparentMatUst = TransparentGirder.transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        ColorChanging();
    }
    public void MoveTransparentGirder()
    {
        //  TransparentGirder.transform.localPosition = new Vector3(0f, -5.7f, girderMovements.girderLocation);
    }

    void ColorChanging()
    {
        if (transform.GetChild(1).childCount > 1)
        {
            if (Mathf.Abs(transform.GetChild(1).transform.localPosition.x) <= 1f && !originalMaterial)
            {
                transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material = GreenMat;
            }
            else if (Mathf.Abs(transform.GetChild(1).transform.localPosition.x) > 1f && !originalMaterial)
            {
                transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material = GirderMat;
            }
        }
    }
    public void BackToOriginalMaterial()
    {
        transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material = GirderMat;
        originalMaterial = true;
    }


}
