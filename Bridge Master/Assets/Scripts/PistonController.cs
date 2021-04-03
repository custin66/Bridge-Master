using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PistonController : MonoBehaviour
{
    [HideInInspector]
    public bool isSwinging = false;

    [SerializeField]
    private float pistonSwingTime, pistonMaxSwingPoint; // pistonun salınım süresi // 1f,3f,1f

    public float pistonDroppingTime; 
    
    public void PistonReturns()
    {
        DOTween.Kill(transform, false);
        Sequence pistonSequence = DOTween.Sequence();
        pistonSequence.Append(transform.DOLocalMoveY(1, pistonDroppingTime))
            .Append(transform.DOLocalMoveY(5, pistonDroppingTime))
            .Append(transform.DOLocalMoveX(0, 0.5f));
    }
    public void PistonMoving() // Kirişi tutan piston sağ sola salınım yapar
    {
        Sequence pistonSequence2 = DOTween.Sequence();
        pistonSequence2.Append(transform.DOLocalMoveX(pistonMaxSwingPoint, pistonSwingTime*0.5f))
            .AppendCallback( () => {
             transform.DOLocalMoveX(-pistonMaxSwingPoint, pistonSwingTime).SetLoops(-1, LoopType.Yoyo); 
        });       
        isSwinging = true;
    }
    public IEnumerator PistonMovingDelayed()
    {
        yield return new WaitForSeconds(5f);
        PistonMoving();
    }
        
}
