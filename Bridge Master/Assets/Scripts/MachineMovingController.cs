using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMovingController : MonoBehaviour
{
    LegSupportController legSupportController;
    PistonController pistonController;

    [HideInInspector]
    public float nextStep; // 10f
    public float machineMovingDuration; //2f

    [SerializeField]
    private float girderLength; //10f 
    private float machineFirstStep = 15f;

    private void Awake()
    {
        legSupportController = FindObjectOfType<LegSupportController>();
        pistonController = FindObjectOfType<PistonController>();
    }
    
    void Start()
    {
        nextStep = transform.position.x + girderLength;
        MachineForwardMoving();
    }

   public void MachineForwardMoving() //Makine 1 adım ileri gider
    {
        Sequence machineSequenceForward = DOTween.Sequence();
        machineSequenceForward.Append(transform.DOMoveX(nextStep, machineMovingDuration))
            .AppendCallback(() => {
                legSupportController.LegSupportOpening();
                })
            .Append(transform.DOMoveX(nextStep + machineFirstStep, machineMovingDuration).SetDelay(legSupportController.legSupportMovingDuration));

        StartCoroutine(pistonController.PistonMovingDelayed());
    }
   public void MachineBackwardMoving() // makine geri yerine gelir  // yeni blok al fonk ile değişecek
    {
        pistonController.PistonReturns();

        Sequence machineSequenceBackward = DOTween.Sequence();
        machineSequenceBackward.Append(transform.DOMoveX(nextStep - girderLength, 1f))
            .AppendCallback(() =>
            {
                legSupportController.LegSupportClosing();
            });
        StartCoroutine(MachineForwardMovingDelayed());
    }
    public IEnumerator MachineForwardMovingDelayed()
    {
        yield return new WaitForSeconds(1f);
        MachineForwardMoving();
    }
    public void MachineForwardMovingNextStep()
    {
        pistonController.PistonReturns();
        legSupportController.LegSupportClosing();
        Sequence machineSequenceNext = DOTween.Sequence();
        machineSequenceNext.Append(transform.DOMoveX(nextStep, machineMovingDuration))
            .AppendCallback(() => {
                legSupportController.LegSupportOpening();
            })
            .Append(transform.DOMoveX(nextStep + machineFirstStep, machineMovingDuration).SetDelay(legSupportController.legSupportMovingDuration));

        //StartCoroutine(pistonController.PistonMovingDelayed());

    }
}
