using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirderStockController : MonoBehaviour
{
    PistonController pistonController;
    GirderMovements girderMovements;

    [SerializeField]
    GameObject Girder;

    // [SerializeField]
    // private List<GameObject> Girders = new List<GameObject>();

    [HideInInspector]
    public Rigidbody girderRigidBody;
    public BoxCollider girderBoxCollider;
    private Vector3 girderLocalPos;
    public Quaternion girderLocalRotation;
    private void Awake()
    {
        girderMovements = FindObjectOfType<GirderMovements>();
        pistonController = FindObjectOfType<PistonController>();
        girderLocalPos = transform.parent.GetChild(1).GetChild(1).transform.localPosition; // - new Vector3(0f, 2.34f, -60f+ girderMovements.girderLocation) ;

        girderRigidBody = transform.parent.GetChild(1).GetChild(1).GetComponent<Rigidbody>();
        girderBoxCollider = transform.parent.GetChild(1).GetChild(1).GetComponent<BoxCollider>();
    }

    public void GirderStockBringing() // Stoktaki kirişlerden birini makinenin kucağına ışınlar
    {
        //  girderLocalPos = new Vector3(0f, -2.29f, -13f + girderMovements.girderLocation);
        //  Girders[Girders.Count - 1].transform.SetParent(transform.parent.GetChild(1).transform);
        //  Girders[Girders.Count - 1].transform.localPosition = girderLocalPos; // smooth hareket ayarlanacak
        Instantiate(Girder, girderLocalPos, girderLocalRotation, transform.parent.GetChild(1).transform);
        transform.parent.GetChild(1).GetChild(1).transform.localScale = Vector3.one;
        girderRigidBody = transform.parent.GetChild(1).GetChild(1).GetComponent<Rigidbody>();
        girderBoxCollider = transform.parent.GetChild(1).GetChild(1).GetComponent<BoxCollider>();
        // Girders.RemoveAt(Girders.Count - 1);
    }
    public IEnumerator GirderStockBringingDelayed()
    {
        yield return new WaitForSeconds(pistonController.pistonDroppingTime * 2f);
        GirderStockBringing();
    }

    public IEnumerator GirderInstantiate()
    {
        yield return new WaitForSeconds(pistonController.pistonDroppingTime * 2f);

    }
}
