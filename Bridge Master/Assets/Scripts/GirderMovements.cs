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

    //[SerializeField]
    //Material lampMaterial;

    [HideInInspector] public int comboCount = 0;

    private GameObject Girder;
    [HideInInspector]
    public bool successedPlayer,successedAI = false;

    [HideInInspector]
    public float girderLocation = 20f;

    public void TimingControl() // Tap timing mekanizmasını kontrol eder
    {
        if (pistonController.isSwinging)
        {
            Girder = transform.GetChild(1).GetChild(1).gameObject;

            pistonController.isSwinging = false;
            StartCoroutine(girderStockController.GirderStockBringingDelayed());

            if (successedPlayer || successedAI)
            {
                GirderSitsToBridge();
                //LampGreen();
                //  MachineEffectController.Instance.tamOturduParticlePlay();
                comboCount++;
            }
            else
            {
                pistonController.PistonReturns();
                StartCoroutine(GirderFellDown());
                // LampRed();
                comboCount = 0;
            }
            machineMovingController.SetCombo();
        }
    }

    void LampDevrim()
    {


    }
    //void LampRed()
    //{
    //    lampMaterial.color = Color.red;
    //}
    //void LampGreen()
    //{
    //    lampMaterial.color = Color.green;

    //}

    void GirderSitsToBridge()
    {
        machineMovingController.nextStep += 10;
        Girder.transform.SetParent(null);
        Sequence girderSequence = DOTween.Sequence();
        girderSequence.Append(Girder.transform.DOLocalMoveY(-5.7f, pistonController.pistonDroppingTime))
                  .Append(Girder.transform.DOLocalMoveZ(girderLocation, 0.1f));
        girderLocation += 10f;
        machineMovingController.MachineForwardMovingNextStep();
        transparentGirderController.MoveTransparentGirder();
    }
    IEnumerator GirderFellDown()
    {
        yield return new WaitForSeconds(pistonController.pistonDroppingTime);
        Girder.transform.SetParent(null);
        girderStockController.girderRigidBody.isKinematic = false;
        girderStockController.girderBoxCollider.isTrigger = true;
        yield return new WaitForSeconds(pistonController.pistonDroppingTime * 0.5f);
        pistonController.PistonMoving();
    }
}
