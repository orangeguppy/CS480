using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslacoilTest : MonoBehaviour
{
    [Header("Targeting")]
    public string targetTag = "Enemy";
    //private Transform target;

    [Header("FX")]
    public ParticleSystem strikePrefab;

    [Header("Turret Stats")]
    public float range;
    public float fireRate = 1f; // higher == faster
    private float fireCooldown = 0f;
    public int damage;

    void Start()
    {
        //InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f); // call UpdateTarget 2x/s
    }

    void Update()
    {


        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;
    }
    /*
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
    }*/

    void Shoot()
    {
        // Find all enemies within range and apply damage + particle effect
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                ParticleAtEnemy(enemy.transform);

                // Deal damage to each enemy
                Damage(enemy.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }

    }

    void ParticleAtEnemy(Transform enemy)
    {
        // Instantiate the particle system at the enemy's position and rotation
        ParticleSystem ln = Instantiate(strikePrefab, enemy.position, Quaternion.identity);
        ln.Play();

        Destroy(ln.gameObject, ln.main.duration);
    }

    // turret range viz onclick
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
