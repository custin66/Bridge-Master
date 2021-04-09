using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirderMovements : MonoBehaviour
{
    MachineMovingController machineMovingController;
    GirderStockController girderStockController;
    PistonController pistonController;

    private GameObject Girder;

    private int girderLocation = 30;

    private void Awake()
    {
        machineMovingController = FindObjectOfType<MachineMovingController>();
        girderStockController = FindObjectOfType<GirderStockController>();
        pistonController = FindObjectOfType<PistonController>();
    }
    public void TimingControl() // Tap timing mekanizmasını kontrol eder
    {
        if (pistonController.isSwinging)
        {
            Girder = transform.GetChild(1).GetChild(1).gameObject;
            
            pistonController.isSwinging = false;
            StartCoroutine(girderStockController.GirderStockBringingDelayed());

            if (Mathf.Abs(transform.GetChild(1).transform.localPosition.x) <= 1f)
            {
                GirderSitsToBridge();
            }
            else
            {
                pistonController.PistonReturns();
                StartCoroutine(GirderFellDown());
            }
        }
    }
    void GirderSitsToBridge()
    {
        machineMovingController.nextStep += 10;
        Girder.transform.SetParent(null);
        Sequence girderSequence = DOTween.Sequence();
        girderSequence.Append(Girder.transform.DOLocalMoveY(0f,pistonController.pistonDroppingTime))
                  .Append(Girder.transform.DOLocalMoveZ(girderLocation, 0.1f));
        girderLocation += 10;
        machineMovingController.MachineForwardMovingNextStep();
    }
    IEnumerator GirderFellDown()
    {
        yield return new WaitForSeconds(pistonController.pistonDroppingTime);
        Girder.transform.SetParent(null);
        girderStockController.girderRigidBody.isKinematic = false;
        pistonController.PistonMoving();
    }
}
