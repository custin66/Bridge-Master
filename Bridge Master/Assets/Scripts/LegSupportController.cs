using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegSupportController : MonoBehaviour
{
    MachineMovingController machineMovingController;
    private Vector3 legSupportLocalPos;
    [HideInInspector]
    public float legSupportMovingDuration = 1f;
    private float rotationAngle = 90f;
    private float legSupportOpenedPos = 9.25f;
    private void Start()
    {
        legSupportLocalPos = transform.localPosition;
    }
    public void LegSupportOpening() //Ön ayak açılır
    {
        if (this.gameObject != null)
        {
            machineMovingController = FindObjectOfType<MachineMovingController>();
            transform.SetParent(null);
            transform.tag = "BackSupport";
            Sequence legSupportSequenceOpening = DOTween.Sequence();
            legSupportSequenceOpening.Append(transform.DOLocalRotate(Vector3.right*rotationAngle, legSupportMovingDuration))
                .Join(transform.DOLocalMoveY(legSupportOpenedPos, legSupportMovingDuration));
        }
    }
    public void LegSupportClosing() //Ön ayak kapanır
    {
        if (this.gameObject != null)
        {
            if (transform.CompareTag("BackSupport"))
            {
                Sequence legSupportSequenceClosing = DOTween.Sequence();
                legSupportSequenceClosing.Append(transform.DOLocalRotate(Vector3.zero, legSupportMovingDuration))
                    .Join(transform.DOLocalMove(legSupportLocalPos, legSupportMovingDuration));
                StartCoroutine(DestroyBackSupport());
            }
            else
            {
                LegSupportOpening();
            }
        }
    }
    private IEnumerator DestroyBackSupport()
    {
        yield return new WaitForSeconds(legSupportMovingDuration);
        Destroy(gameObject);
    }
}

