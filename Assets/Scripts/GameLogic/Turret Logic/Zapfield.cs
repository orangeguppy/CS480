using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapfield : MonoBehaviour
{
    [Header("Targeting")]
    public string targetTag = "Enemy";
    private List<Enemy> enemiesInRange = new List<Enemy>();

    [Header("Turret Parts")]
    public Transform firingPoint;
    public GameObject lineRendererPrefab; // Prefab for the line renderer

    private List<LineRenderer> activeLineRenderers = new List<LineRenderer>(); // Active lasers

    [Header("Turret Stats")]
    public float range = 5f;
    public int damage;
    public float slowAmt; // 1 = stun

    void Start()
    {
        InvokeRepeating(nameof(UpdateTargets), 0f, 0.5f); // Call UpdateTargets 2x/s
    }

    void Update()
    {
        if (enemiesInRange.Count == 0)
        {
            ClearAllLineRenderers(); // No enemies, stop the lasers
            return;
        }

        ShootLaserAtAll();
    }

    void UpdateTargets()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        enemiesInRange.Clear(); // Clear previous list of enemies

        foreach (GameObject enemyObj in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyObj.transform.position);
            if (distanceToEnemy <= range)
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }

        // If there are more enemies in range than active line renderers, instantiate more line renderers
        while (activeLineRenderers.Count < enemiesInRange.Count)
        {
            GameObject newLineRendererObj = Instantiate(lineRendererPrefab);
            LineRenderer lineRenderer = newLineRendererObj.GetComponent<LineRenderer>();
            activeLineRenderers.Add(lineRenderer);
        }

        // If there are fewer enemies than line renderers, disable the extra line renderers
        for (int i = enemiesInRange.Count; i < activeLineRenderers.Count; i++)
        {
            activeLineRenderers[i].enabled = false;
        }
    }

    // Turret range viz onclick
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void ShootLaserAtAll()
    {
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            Enemy enemy = enemiesInRange[i];
            if (enemy != null)
            {
                // Deal damage and slow the enemy
                enemy.TakeDamage(damage * Time.deltaTime); // DoT effect
                enemy.Slow(slowAmt);

                // Enable and set positions for the line renderer
                LineRenderer lineRenderer = activeLineRenderers[i];
                if (!lineRenderer.enabled) lineRenderer.enabled = true;

                lineRenderer.SetPosition(0, firingPoint.position);
                lineRenderer.SetPosition(1, enemy.transform.position);
            }
        }
    }

    void ClearAllLineRenderers()
    {
        foreach (LineRenderer lr in activeLineRenderers)
        {
            if (lr != null)
            {
                lr.enabled = false; // Disable the line renderer without destroying it
            }
        }
    }
}
