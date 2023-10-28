using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSkillController : MonoBehaviour
{
    [SerializeField] private RotateSaw outerSaw;
    [SerializeField] private RotateSaw innerSaw;
    [SerializeField] private float timeToPhase2;
    [SerializeField] private float timeToPhase3;
    [SerializeField] private float phase2RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PhaseTimer());
    }


    private IEnumerator PhaseTimer()
    {
        yield return new WaitForSeconds(timeToPhase2);
        outerSaw.rotateSpeed = -phase2RotateSpeed;
        innerSaw.rotateSpeed = -phase2RotateSpeed;
        yield return new WaitForSeconds(timeToPhase3);
        innerSaw.rotateSpeed = phase2RotateSpeed;
    }
}
