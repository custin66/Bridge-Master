using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirderStockController : MonoBehaviour
{
    PistonController pistonController;

    [SerializeField]
    private List<GameObject> Girders = new List<GameObject>();

    [HideInInspector]
    public Rigidbody girderRigidBody;
    private Vector3 girderLocalPos;
    private void Awake()
    {
        pistonController = FindObjectOfType<PistonController>();
        girderLocalPos = transform.parent.GetChild(1).GetChild(1).transform.localPosition;
        girderRigidBody = transform.parent.GetChild(1).GetChild(1).GetComponent<Rigidbody>();
    }

    public void GirderStockBringing() // Stoktaki kirişlerden birini makinenin kucağına ışınlar
    {
        Girders[Girders.Count - 1].transform.SetParent(transform.parent.GetChild(1).transform);
        Girders[Girders.Count - 1].transform.localPosition = girderLocalPos; // smooth hareket ayarlanacak
        girderRigidBody = transform.parent.GetChild(1).GetChild(1).GetComponent<Rigidbody>();
        Girders.RemoveAt(Girders.Count - 1);
    }
    public IEnumerator GirderStockBringingDelayed()
    {
        yield return new WaitForSeconds(pistonController.pistonDroppingTime*1.5f);
        GirderStockBringing();
    }
}
