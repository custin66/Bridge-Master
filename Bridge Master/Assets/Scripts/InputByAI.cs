using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputByAI : MonoBehaviour
{
    [SerializeField] GirderMovements girderMovements;

    [Range(0, 100)]
    [SerializeField] private int successRate;
    public float AILoopTime;
 
    void Start()
    {
       StartCoroutine(RandomizeSuccess());
    } 

    IEnumerator RandomizeSuccess()
    {
        while (true)
        {
        yield return new WaitForSeconds(AILoopTime);
        int rate = Random.Range(0, 100);
        girderMovements.successedAI = false;
        if (rate < successRate)
        {
            girderMovements.successedAI = true;
        }
            if (girderMovements.successedAI)
            {
                StartCoroutine(TrueHitByAI());
            }
            else
            {
                StartCoroutine(FalseHitByAI());
            }
        }
    }

    IEnumerator TrueHitByAI()
    {
        yield return new WaitUntil(() => Mathf.Abs(transform.GetChild(1).transform.localPosition.x) <= 0.75f);
        girderMovements.TimingControl();
    } 
    IEnumerator FalseHitByAI()
    {
        yield return new WaitUntil(() => Mathf.Abs(transform.GetChild(1).transform.localPosition.x) > 1.5f);
        girderMovements.TimingControl();
    }
    
}
