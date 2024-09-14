using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBBB : MonoBehaviour
{
    // custom turret script for bb

    [Header("Targeting")]
    public string targetTag = "Enemy";
    private Transform target;

    [Header("Turret Parts")]
    public Transform rotatePart;
    public Transform bulletPrefab;
    public Transform firingPoint1;
    public Transform firingPoint2;

    [Header("Turret Stats")]
    public float range = 5f;
    public float fireRate = 1f; // higher == faster
    private float fireCooldown = 0f;
    public int damage = 5;

    private bool useFiringPoint1 = true; // Keeps track of which firing point to use

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f); // call UpdateTarget 2x/s
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        RotateTowardsTarget();

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;
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

        target = nearestEnemy != null && shortestDistance <= range ? nearestEnemy.transform : null;
    }

    //rotato potato
    void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = lookRotation.eulerAngles;
        rotatePart.rotation = Quaternion.Euler(0f, rotation.y - 180, 0f);
    }

    void Shoot()
    {
        // Alternate between firingPoint1 and firingPoint2
        Transform chosenFiringPoint = useFiringPoint1 ? firingPoint1 : firingPoint2;
        useFiringPoint1 = !useFiringPoint1; // Toggle between firing points

        GameObject bulletObject = Instantiate(bulletPrefab, chosenFiringPoint.position, chosenFiringPoint.rotation).gameObject;
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Hit(target);
        }
    }

    // turret range viz onclick
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

