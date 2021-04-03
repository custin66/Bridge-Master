using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegSupportController : MonoBehaviour
{
    private GameObject ElephantMachine;
    private Vector3 legSupportLocalPos;
    private float legSupportOpenedPos = 3f;
    [HideInInspector]
    public float legSupportMovingDuration = 1f;
    private float rotationAngle = 90f;
    private void Awake()
    {
        ElephantMachine = gameObject.transform.parent.gameObject;
        legSupportLocalPos = transform.localPosition;
    }
    public void LegSupportOpening() //Ön ayak açılır
    {
        transform.SetParent(null);
        Sequence legSupportSequenceOpening = DOTween.Sequence();
        legSupportSequenceOpening.Append(transform.DOLocalRotate(Vector3.zero, legSupportMovingDuration))
            .Join(transform.DOLocalMoveY(legSupportOpenedPos, legSupportMovingDuration));
    }
    public void LegSupportClosing() //Ön ayak kapanır
    {
        transform.SetParent(ElephantMachine.transform);
        Sequence legSupportSequenceClosing = DOTween.Sequence();
        legSupportSequenceClosing.Append(transform.DOLocalRotate(Vector3.forward* rotationAngle, legSupportMovingDuration))
            .Join(transform.DOLocalMove(legSupportLocalPos, legSupportMovingDuration));
    }
    public void LegSupportMovingNext()
    {

    }
}

