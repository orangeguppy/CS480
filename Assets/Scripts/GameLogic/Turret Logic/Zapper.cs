using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : MonoBehaviour
{
    [Header("Targeting")]
    public string targetTag = "Enemy";
    private Transform target;
    private Enemy targetEnemy;

    [Header("Turret Parts")]
    public Transform firingPoint;
    public LineRenderer lineRenderer;

    [Header("Turret Stats")]
    public float range = 5f;
    public int damage;
    public float slowAmt; // 1 = stun

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f); // call UpdateTarget 2x/s
    }

    void Update()
    {
        if (target == null)
        {
            if(lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            return;  //stop muzzle flash & reset when curr target dies/outofrange
        }

        shootLaser();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //target = nearestEnemy != null && shortestDistance <= range ? nearestEnemy.transform : null;
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy> ();
        } else
        {
            target = null;
        }
    }

    // turret range viz onclick
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void shootLaser()
    {
        targetEnemy.TakeDamage(damage * Time.deltaTime); //DoT effect
        targetEnemy.Slow(slowAmt);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firingPoint.position);
        lineRenderer.SetPosition(1, target.position);
    }
}
