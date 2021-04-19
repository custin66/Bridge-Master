using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PistonController : MonoBehaviour
{
    MachineMovingController machineMovingController;
    LegSupportController legSupportController;

    [HideInInspector]
    public bool isSwinging = false;

    [SerializeField]
    private float pistonSwingTime, pistonMaxSwingPoint; // pistonun salınım süresi // 1f,3f,1f

    public float pistonDroppingTime;

    private void Start()
    {
        machineMovingController = FindObjectOfType<MachineMovingController>();
        legSupportController = FindObjectOfType<LegSupportController>();
    }
    public void PistonReturns()
    {
        DOTween.Kill(transform, false);
        Sequence pistonSequence = DOTween.Sequence();
        pistonSequence.Append(transform.DOLocalMoveY(-2.8f, pistonDroppingTime).SetEase(Ease.Linear))
            .Append(transform.DOLocalMove(new Vector3(pistonMaxSwingPoint, 0f, 0f), pistonDroppingTime));
            //.Append(transform.DOLocalMoveY(0f, pistonDroppingTime).SetEase(Ease.Linear))
           // .Join(transform.DOLocalMoveX(pistonMaxSwingPoint, pistonDroppingTime).SetEase(Ease.Linear));
    }
    public void PistonMoving() // Kirişi tutan piston sağ sola salınım yapar
    {
        transform.DOLocalMoveX(-pistonMaxSwingPoint, pistonSwingTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);    
        isSwinging = true;
    }
    public IEnumerator PistonMovingDelayed()
    {
        yield return new WaitForSeconds(legSupportController.legSupportMovingDuration+machineMovingController.machineMovingDuration);
        PistonMoving();
    }
    public IEnumerator PistonFirstMoveDelayed()
    {
        yield return new WaitForSeconds(legSupportController.legSupportMovingDuration+machineMovingController.machineMovingDuration*2f);
        PistonMoving();
    }
        
}
