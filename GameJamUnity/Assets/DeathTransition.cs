using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class DeathTransition : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;
    [SerializeField] private float volumeWeight;
    [SerializeField] private Vector2 targetCenterX;
    [SerializeField] private RawImage portal;
    [SerializeField] private float weightIncreaseRate;
    [SerializeField] private float centerXIncreaseRate;
    [SerializeField] private float howLongToReachEndCenterX;
    [SerializeField] private float opacityIncreaseRate;

    private bool transitionInProgress = false;
    private bool transitionFinish = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!transitionInProgress && (collision.CompareTag("Enemy") || collision.CompareTag("Chakram") || collision.CompareTag("Sawblade")))
        {
            StartCoroutine(StartTransition());
        }

        if (transitionFinish)
            StopCoroutine(StartTransition());
    }

    private IEnumerator StartTransition()
    {
        transitionInProgress = true;

        // Increase the volume weight
        while (globalVolume.weight < volumeWeight)
        {
            globalVolume.weight += weightIncreaseRate * Time.deltaTime;
            yield return null;
        }

        float elapsedTime = 0f;
        while (elapsedTime < howLongToReachEndCenterX)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / howLongToReachEndCenterX;

            // Apply the easing function to t
            t = EaseInOut(t);

            globalVolume.profile.TryGet(out LensDistortion lensDistortion);
            lensDistortion.center.value =  Vector2.Lerp(new Vector2(0.5f,0.5f),targetCenterX, t);
            yield return null;
        }

        // Increase opacity of the portal
        while (portal.color.a < 1)
        {
            Color newColor = portal.color;
            newColor.a += opacityIncreaseRate * Time.deltaTime;
            portal.color = newColor;
            yield return null;
        }

        // Decrease the volume weight
        while (globalVolume.weight > 0)
        {
            globalVolume.weight -= weightIncreaseRate * Time.deltaTime;
            yield return null;
        }

        //// Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        while (globalVolume.weight < volumeWeight)
        {
            globalVolume.weight += weightIncreaseRate * Time.deltaTime;
            yield return null;
        }
        //enabled = false;
        //// Reset the flag to allow the transition to start again
        //transitionFinish = true;

        Debug.Log("here");

        // Exit the coroutine after the last Debug.Log
        yield break;
    }

    float EaseInOut(float t)
    {
        return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }
}


