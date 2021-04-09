using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMovingController : MonoBehaviour
{
    LegSupportController legSupportController;
    LegSupportController legSupportController2;
    PistonController pistonController;

    public GameObject legSupportPrefab;
    public Quaternion legSupportLocalRotation;

    [HideInInspector]
    public float nextStep; // 10f
    public float machineMovingDuration; //2f

    [SerializeField]
    private float girderLength; //10f 
    private float machineFirstStep = 15f;

    private void Awake()
    {
        pistonController = FindObjectOfType<PistonController>();
        Instantiate(legSupportPrefab, new Vector3(0f, 10.8f, 60f), legSupportLocalRotation, transform.GetChild(2));
    }

    void Start()
    {
        nextStep = transform.position.z + girderLength;
        MachineForwardMoving();
    }

    public void MachineForwardMoving() //Makine 1 adım ileri gider
    {
        legSupportController = gameObject.transform.GetChild(2).GetChild(0).transform.GetComponent<LegSupportController>();

        Sequence machineSequenceForward = DOTween.Sequence();
        machineSequenceForward.Append(transform.DOMoveZ(nextStep, machineMovingDuration))
        .AppendCallback(() =>
        {
            legSupportController.LegSupportOpening();
        })
            .Append(transform.DOMoveZ(nextStep + machineFirstStep, machineMovingDuration).SetDelay(legSupportController.legSupportMovingDuration));

        StartCoroutine(pistonController.PistonMovingFirstDelayed());
    }
    public void MachineForwardMovingNextStep()
    {
        Instantiate(legSupportPrefab, new Vector3(0, 10.8f, 60f + nextStep), legSupportLocalRotation, transform.GetChild(2));
        if (GameObject.FindGameObjectWithTag("BackSupport") != null || gameObject.transform.GetChild(2).GetChild(0) != null)
        {
            legSupportController = GameObject.FindGameObjectWithTag("BackSupport").transform.GetComponent<LegSupportController>();
            legSupportController2 = gameObject.transform.GetChild(2).GetChild(0).transform.GetComponent<LegSupportController>();

            pistonController.PistonReturns();
            legSupportController.LegSupportClosing();
            legSupportController2.LegSupportClosing();
            legSupportController.LegSupportOpening();

            transform.DOMoveZ(transform.position.z + girderLength, machineMovingDuration).SetDelay(legSupportController.legSupportMovingDuration);

            StartCoroutine(pistonController.PistonMovingDelayed());
        }

    }
}
