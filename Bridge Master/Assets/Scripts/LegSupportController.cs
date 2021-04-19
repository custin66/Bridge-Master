using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegSupportController : MonoBehaviour
{
    //  MachineMovingController machineMovingController;
    private Vector3 legSupportLocalPos;
    [HideInInspector]
    public float legSupportMovingDuration = 1f;
    private float rotationAngle = 90f;
    private float legSupportOpenedPos = -2.82f;
    private void Start()
    {
        legSupportLocalPos = transform.localPosition;
    }
    public void LegSupportCompareTag() // Tagını kontrol eder
    {
        if (this.gameObject != null)
        {
            if (transform.CompareTag("BackSupport"))
            {
                LegSupportClosing();
            }
            else if (transform.CompareTag("FrontSupport"))
            {
                LegSupportOpening();
            }
        }
    }
    void LegSupportOpening() //Ön ayak açılır
    {
        if (this.gameObject != null)
        {
            transform.SetParent(null);
            transform.tag = "BackSupport";
            Sequence legSupportSequenceOpening = DOTween.Sequence();
            legSupportSequenceOpening.Append(transform.DOLocalRotate(Vector3.zero, legSupportMovingDuration))
                .Join(transform.DOLocalMoveY(legSupportOpenedPos, legSupportMovingDuration));
        }
    }
    void LegSupportClosing() //Ön ayak kapanır
    {
        Sequence legSupportSequenceClosing = DOTween.Sequence();
        legSupportSequenceClosing.Append(transform.DORotate(Vector3.right * rotationAngle, legSupportMovingDuration).SetEase(Ease.Linear))
            .Join(transform.DOLocalMoveY(0f, legSupportMovingDuration).SetEase(Ease.Linear));
        StartCoroutine(DestroyBackSupport());
    }
    private IEnumerator DestroyBackSupport()
    {
        yield return new WaitForSeconds(legSupportMovingDuration);
        Destroy(gameObject);
    }
}

