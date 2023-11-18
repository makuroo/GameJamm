using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PhaseController : MonoBehaviour
{
    [SerializeField] private HookMekanik grapplingSkill;
    [SerializeField] private SawSkillController sawSkill;
    [SerializeField] private GameObject sawGO;
    [SerializeField] private SpawnChakram chakramSkill;
    [SerializeField] private GameObject tbc;
    public PlayableDirector timeline;
    
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
            Destroy(GetComponent<LineRenderer>());
            if (grapplingSkill.currentChain != null)
                Destroy(grapplingSkill.currentChain);
            transform.position = Vector2.zero;
            chakramSkill.enabled = true;
        }

        if(hitCount == 3)
        {
            sawGO.SetActive(true);
            sawSkill.enabled = true;
        }
        if(hitCount == 4)
        {
            tbc.SetActive(true);
           // timeline.Play();

            Invoke("sceneganti", 2f);
        }
    }

    public void sceneganti()
    {
        SceneManager.LoadScene(0);
    }
}
