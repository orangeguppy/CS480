using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [Header("Targeting")]
    public string targetTag = "Enemy";
    private Transform target;

    [Header("Turret Parts")]
    public Transform rotatePart;
    public Transform firingPoint;
    public ParticleSystem flashPrefab;

    [Header("Turret Stats")]
    public float range = 5f;
    public float fireRate = 1f; // higher == faster
    private float fireCooldown = 0f;
    public int damage = 5;

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f); // call UpdateTarget 2x/s
    }

    void Update()
    {
        if (target == null)
        {
            return;  //stop muzzle flash & reset when curr target dies/outofrange
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
        float shortestDistanceToCloaked = Mathf.Infinity;
        float shortestDistanceToUncloaked = Mathf.Infinity;
        GameObject cloakedEnemy = null;
        GameObject uncloakedEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript == null) continue;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (enemyScript.isCloaked && distanceToEnemy < shortestDistanceToCloaked)
            {
                shortestDistanceToCloaked = distanceToEnemy;
                cloakedEnemy = enemy;
            }
            else if (!enemyScript.isCloaked && distanceToEnemy < shortestDistanceToUncloaked)
            {
                shortestDistanceToUncloaked = distanceToEnemy;
                uncloakedEnemy = enemy;
            }
        }

        // Prioritize cloaked enemy if found within range, otherwise use nearest uncloaked enemy
        if (cloakedEnemy != null && shortestDistanceToCloaked <= range)
        {
            target = cloakedEnemy.transform;
        }
        else if (uncloakedEnemy != null && shortestDistanceToUncloaked <= range)
        {
            target = uncloakedEnemy.transform;
        }
        else
        {
            target = null;
        }
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
        ActivateFlashEffect();
        Damage(target);
        SetEnemyOnFire(target);
    }



    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }

    void ActivateFlashEffect()
    {
        ParticleSystem flash = Instantiate(flashPrefab, firingPoint.position, Quaternion.identity);
        flash.Play();

        Destroy(flash.gameObject, flash.main.duration);
    }

    void SetEnemyOnFire(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.onFire = true; // Set the enemy on fire
        }
    }

    // turret range viz onclick
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
