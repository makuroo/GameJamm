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
    [SerializeField] private float timeToLastTransition = 1;

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
            t = EaseInOut(t);

            globalVolume.profile.TryGet(out LensDistortion lensDistortion);
            lensDistortion.center.value =  Vector2.Lerp(new Vector2(0.5f,0.5f),targetCenterX, t);
            yield return null;
        }

        while (portal.color.a < 1)
        {
            Color newColor = portal.color;
            newColor.a += opacityIncreaseRate * Time.deltaTime;
            portal.color = newColor;
            yield return null;
        }

        while (globalVolume.weight > 0)
        {
            globalVolume.weight -= weightIncreaseRate * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(timeToLastTransition);
        while (globalVolume.weight < volumeWeight)
        {
            globalVolume.weight += weightIncreaseRate * Time.deltaTime;
            yield return null;
        }

        Debug.Log("here");

        yield break;
    }

    float EaseInOut(float t)
    {
        return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }
}


