using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMekanik : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject chainPrefabs;
    [SerializeField] private GameObject[] trees;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float chainSpeed = 5f;
    private LineRenderer lr;

    void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 2;

        StartCoroutine(InstantiateChainWithCooldown());
    }

    void UpdateLineRenderer(Vector3 startPos, Vector3 endPos)
    {
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }

    IEnumerator InstantiateChainWithCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            GameObject closestTree = FindClosestTree();
            if (closestTree != null)
            {
                GameObject chainInstance = Instantiate(chainPrefabs, transform.position, Quaternion.identity);
                Rigidbody2D rb = chainInstance.GetComponent<Rigidbody2D>();
                rb.velocity = (closestTree.transform.position - transform.position).normalized * chainSpeed;

                StartCoroutine(FreezeChainWhenClose(chainInstance, closestTree));
            }
        }
    }

    IEnumerator FreezeChainWhenClose(GameObject chain, GameObject targetTree)
    {
        while (true)
        {
            float distance = Vector3.Distance(chain.transform.position, targetTree.transform.position);
            if (distance < 0.5f)
            {
                Rigidbody2D rb = chain.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                UpdateLineRenderer(transform.position, chain.transform.position);

                yield return StartCoroutine(MoveTowardsHook(chain.transform.position, chain));

                Destroy(chain);

                yield break;
            }
            yield return null;
        }
    }

    IEnumerator MoveTowardsHook(Vector3 hookPosition, GameObject chain)
    {
        while (Vector3.Distance(transform.position, hookPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, hookPosition, moveSpeed * Time.deltaTime);
            UpdateLineRenderer(transform.position, chain.transform.position);
            yield return null;
        }
    }

    GameObject FindClosestTree()
    {
        GameObject closestTree = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(player.transform.position, tree.transform.position);
            if (distance < closestDistance)
            {
                closestTree = tree;
                closestDistance = distance;
            }
        }

        return closestTree;
    }
}