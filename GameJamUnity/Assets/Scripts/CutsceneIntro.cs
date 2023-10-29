using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneIntro : MonoBehaviour
{
    public Animator animatorCutscene;
    private bool gembok = true;

    private void Start()
    {
        Invoke("SpaceFunc", 7f);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gembok)
        {
            animatorCutscene.SetBool("Space", true);

        }
    }

    private void SpaceFunc()
    {
        Debug.Log("Now u can press Space");
        gembok = false;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("HasBall", 0);
    }


}
