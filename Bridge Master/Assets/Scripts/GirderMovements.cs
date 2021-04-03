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

    private int girderLocation = 15;

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
            Girder = transform.GetChild(1).GetChild(0).gameObject;
            
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
        Girder.transform.SetParent(null);
        Sequence girderSequence = DOTween.Sequence();
        girderSequence.Append(Girder.transform.DOLocalMoveY(0f, 1f))
                  .Append(Girder.transform.DOLocalMoveX(girderLocation, 0.1f));
        machineMovingController.nextStep += 10;
        girderLocation += 10;
        machineMovingController.MachineForwardMovingNextStep();
       // machineMovingController.MachineBackwardMoving();
    }
    IEnumerator GirderFellDown()
    {
        yield return new WaitForSeconds(pistonController.pistonDroppingTime);
        Girder.transform.SetParent(null);
        girderStockController.girderRigidBody.isKinematic = false;
        pistonController.PistonMoving();
    }
}
