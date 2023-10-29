using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour
{

    public DeathTransition transition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            if (PlayerPrefs.GetInt("HasBall") == 1)
            {
                transition.targetScene = "Monster_1";
                transition.StartCoroutine(transition.StartTransition());
            }
            else
            {
                transition.targetScene = "Lab_2030";
                transition.StartCoroutine(transition.StartTransition());
            }
        }
    }
}
