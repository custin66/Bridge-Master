using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirderStockController : MonoBehaviour
{
    [SerializeField] PistonController pistonController;
    [SerializeField] GirderMovements girderMovements;

    
    public GameObject Girder;

    [HideInInspector]
    public Rigidbody girderRigidBody;
    public BoxCollider girderBoxCollider;
    private Vector3 girderLocalPos;
    public Quaternion girderLocalRotation;
    private void Awake()
    {
        girderLocalPos = new Vector3(0f, 0f, -22.3f); //transform.parent.GetChild(1).GetChild(1).transform.localPosition;
                                                      // girderRigidBody = transform.parent.GetChild(1).GetChild(1).GetComponent<Rigidbody>();
                                                      //  girderBoxCollider = transform.parent.GetChild(1).GetChild(1).GetComponent<BoxCollider>();
    }

    public void GirderStockBringing() // makinenin pistonuna yeni kiriş doğurur
    {
        Instantiate(Girder, girderLocalPos, girderLocalRotation, transform.parent.GetChild(1).transform);
        transform.parent.GetChild(1).GetChild(1).transform.localScale = Vector3.one;
        transform.parent.GetChild(1).GetChild(1).transform.localPosition = girderLocalPos;
        girderRigidBody = transform.parent.GetChild(1).GetChild(1).GetComponent<Rigidbody>();
        girderBoxCollider = transform.parent.GetChild(1).GetChild(1).GetComponent<BoxCollider>();
    }
    public IEnumerator GirderStockBringingDelayed()
    {
        yield return new WaitForSeconds(pistonController.pistonDroppingTime * 2f);
        GirderStockBringing();
    }
}
