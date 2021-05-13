using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class GirderMovements : MonoBehaviour
{
    [SerializeField] MachineMovingController machineMovingController;
    [SerializeField] GirderStockController girderStockController;
    [SerializeField] PistonController pistonController;
    [SerializeField] TransparentGirderController transparentGirderController;
    [SerializeField] MachineEffectController machineEffectController;

    [HideInInspector] public int comboCount = 0;
    [SerializeField] private int bridgeCount;
    [SerializeField] private GameObject ComboBarSlider;
    [SerializeField] private GameObject FracturedGirder;
    [SerializeField] private GameObject FracturedGirdersParent;
    [SerializeField] private ProgressBar myBar;

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
        ComboBarController();
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
                if (myBar != null)
                {
                    myBar.currentPercent = 100f;
                }
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
        Instantiate(FracturedGirder, Girder.transform.position, Quaternion.identity, FracturedGirdersParent.transform);
        Destroy(Girder.gameObject);
        //Girder.transform.SetParent(null);
        girderStockController.girderRigidBody.isKinematic = false;
        girderStockController.girderBoxCollider.isTrigger = true;
        yield return new WaitUntil(() => Mathf.Abs(transform.GetChild(1).transform.localPosition.x) == pistonController.pistonMaxSwingPoint);
        pistonController.PistonMoving();
        yield return new WaitForSeconds(3f);
        Destroy(FracturedGirdersParent.transform.GetChild(0).gameObject);
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
    void ComboBarController()
    {
        if (ComboBarSlider != null)
        {
            if (comboCount > 1)
            {
                ComboBarSlider.SetActive(true);
            }
            else
            {
                ComboBarSlider.SetActive(false);
            }
        }
    }
}
