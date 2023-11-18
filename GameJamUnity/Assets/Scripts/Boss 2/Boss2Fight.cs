using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Fight : MonoBehaviour
{
    public bool isPhase1 = true;
    public bool isPhase2 = false;
    public bool isPhase3 = false;
    private Collider2D col;
    [SerializeField] private smallProjectiles smallProjectileScript;

    [Header("Small Projectile Settings")]
    [SerializeField] private GameObject smallProjectilePrefab;
    [SerializeField] private int numProjectiles = 8;
    [SerializeField] private float spreadAngle = 360f;
    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float shootingSpeed = 5f;
    [SerializeField] private float shootInterval = 6f;

    private bool isFiring = false;

    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        col.enabled = false;
        Invoke("enablingCollider", 1);
    }

    private void Update()
    {
        if (isPhase3 && !isFiring)
        {
            StartCoroutine(fireworkSkill());
        }
    }

    private void enablingCollider()
    {
        col.enabled = true;
    }

    private void ShootMultiple()
    {
        float angleStep = spreadAngle / numProjectiles;

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle)).normalized;
            Vector2 shootingPosition = (Vector2)transform.position + (direction * offsetDistance);

            GameObject projectile = Instantiate(smallProjectilePrefab, shootingPosition, Quaternion.identity);
            smallProjectileScript = projectile.GetComponent<smallProjectiles>();

            if (smallProjectileScript != null)
            {
                smallProjectileScript.SetInitialDirection(direction);
            }
        }
    }

    private IEnumerator fireworkSkill()
    {
        isFiring = true;

        while (true)
        {
            ShootMultiple();
            yield return new WaitForSeconds(shootInterval);
            smallProjectileScript.ReturnToBoss();
        }

        isFiring = false;
    }
}
