using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMovingController : MonoBehaviour
{

    LegSupportController legSupportController, legSupportController2;
    [SerializeField] PistonController pistonController;
    [SerializeField] GirderMovements girderMovements;

    // [SerializeField] private GameObject legSupportsParent;
    public GameObject legSupportPrefab;
    public Quaternion legSupportLocalRotation;
    [SerializeField] private int legSupportLayer;

    [HideInInspector]
    public float nextStep; // 10f
    public float machineMovingDuration; //2f
    private float maxDuration; //2f

    [SerializeField]
    private float girderLength; //10f 
    private float machineFirstStep = 10f;

    private void Awake()
    {
        Instantiate(legSupportPrefab, new Vector3(transform.position.x, 0f, 26f), legSupportLocalRotation, transform.GetChild(2));
        legSupportPrefab.layer = legSupportLayer;
        maxDuration = machineMovingDuration;
    }

    void Start()
    {
        nextStep = transform.position.z + girderLength;
        StartCoroutine(StartMachine());
    }


    public void MachineFirstMove() // Makine ilk yürüyüşünü yapar
    {
        legSupportController = gameObject.transform.GetChild(2).GetChild(0).transform.GetComponent<LegSupportController>();

        Sequence machineSequenceForward = DOTween.Sequence();
        machineSequenceForward.Append(transform.DOMoveZ(nextStep, machineMovingDuration))
        .AppendCallback(() =>
        {
            legSupportController.LegSupportCompareTag();
        })
            .Append(transform.DOMoveZ(nextStep + machineFirstStep, machineMovingDuration).SetDelay(legSupportController.legSupportMovingDuration));

        StartCoroutine(pistonController.PistonFirstMoveDelayed());
    }
    public void MachineForwardMovingNextStep() //Makine 1 adım ileri gider
    {
        Instantiate(legSupportPrefab, new Vector3(transform.position.x, -2.82f, 26f + nextStep), legSupportLocalRotation, transform.GetChild(2));
        legSupportPrefab.layer = legSupportLayer;
        if (GameObject.FindGameObjectWithTag("BackSupport") != null || gameObject.transform.GetChild(2).GetChild(0) != null)
        {
            legSupportController = gameObject.transform.parent.GetChild(1).GetChild(0).transform.GetComponent<LegSupportController>();
            legSupportController2 = gameObject.transform.GetChild(2).GetChild(0).transform.GetComponent<LegSupportController>();

            pistonController.PistonReturns();
            legSupportController.LegSupportCompareTag();
            legSupportController2.LegSupportCompareTag();

            transform.DOMoveZ(transform.position.z + girderLength, machineMovingDuration).SetDelay(legSupportController.legSupportMovingDuration);

            StartCoroutine(pistonController.PistonMovingDelayed());
        }
    }
    public void SetCombo()
    {
        machineMovingDuration = maxDuration - girderMovements.comboCount * 0.1f;
    }

    private IEnumerator StartMachine()
    {
        yield return new WaitUntil(() => UIManager.Instance.startGame);
        MachineFirstMove();
    }
}
