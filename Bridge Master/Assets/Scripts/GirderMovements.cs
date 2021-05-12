using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirderMovements : MonoBehaviour
{
    [SerializeField] MachineMovingController machineMovingController;
    [SerializeField] GirderStockController girderStockController;
    [SerializeField] PistonController pistonController;
    [SerializeField] TransparentGirderController transparentGirderController;
    [SerializeField] MachineEffectController machineEffectController;

    [HideInInspector] public int comboCount = 0;
    [SerializeField] private int bridgeCount;

    [HideInInspector] public GameObject Girder;
    [HideInInspector] public bool successedPlayer, successedAI = false;

    [HideInInspector]
    public float girderLocation = 20f;

    void Awake()
    {
        girderStockController.GirderStockBringing();
    }

    void Update()
    {
        FinishControl();
    }
    public void TimingControl() // Tap timing mekanizmasını kontrol eder
    {
        if (pistonController.isSwinging)
        {
            Girder = transform.GetChild(1).GetChild(1).gameObject;

            pistonController.isSwinging = false;
            StartCoroutine(girderStockController.GirderStockBringingDelayed());

            if (successedPlayer || successedAI)
            {
                transparentGirderController.BackToOriginalMaterial();
                GirderSitsToBridge();
                machineEffectController.TrueHitParticlePlay();
                comboCount++;
                bridgeCount--;
            }
            else
            {
                pistonController.PistonReturns();
                StartCoroutine(GirderFellDown());
                comboCount = 0;
            }
            machineMovingController.SetCombo();
        }
    }
    void GirderSitsToBridge()
    {
        machineMovingController.nextStep += 10;
        Girder.transform.SetParent(null);
        Sequence girderSequence = DOTween.Sequence();
        girderSequence.Append(Girder.transform.DOLocalMoveY(-5.7f, pistonController.pistonDroppingTime))
                  .Append(Girder.transform.DOLocalMoveZ(girderLocation, 0.1f));
        girderLocation += 10f;
        machineMovingController.MachineForwardMovingNextStep();
        transparentGirderController.originalMaterial = false;
    }
    IEnumerator GirderFellDown()
    {
       // transparentGirderController.BackToOriginalMaterial();
       // transparentGirderController.originalMaterial = false;
        yield return new WaitForSeconds(pistonController.pistonDroppingTime);
        Girder.transform.SetParent(null);
        girderStockController.girderRigidBody.isKinematic = false;
        girderStockController.girderBoxCollider.isTrigger = true;
        yield return new WaitForSeconds(pistonController.pistonDroppingTime * 0.5f);
        pistonController.PistonMoving();
    }
    void FinishControl()
    {
        if (bridgeCount == 0)
        {
            if (gameObject.layer == 3)
            {
                UIManager.Instance.OpenFinishPanel();
            }
            else if (gameObject.layer == 6)
            {
                UIManager.Instance.OpenReplayPanel();

            }
        }
    }
}
