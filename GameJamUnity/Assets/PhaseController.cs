using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    [SerializeField] private HookMekanik grapplingSkill;
    [SerializeField] private SawSkillController sawSkill;
    [SerializeField] private GameObject sawGO;
    [SerializeField] private SpawnChakram chakramSkill;
    public int hitCount = 0;

    private void Update()
    {
        if (hitCount == 1)
        {
            grapplingSkill.enabled = true;
        }

        if (hitCount == 2)
        {
            grapplingSkill.StopAllCoroutines();
            grapplingSkill.enabled = false;
            transform.position = Vector2.zero;
            chakramSkill.enabled = true;
        }

        if(hitCount == 3)
        {
            sawGO.SetActive(true);
            sawSkill.enabled = true;
        }
    }

}
