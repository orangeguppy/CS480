using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : MonoBehaviour
{
    [Header("Targeting")]
    public string targetTag = "Enemy";
    private Transform target;

    [Header("Turret Parts")]
    public Transform rotatePart;
    public Transform firingPoint;
    public ParticleSystem flash;
    public GameObject cyl;

    [Header("Turret Stats")]
    public float range = 5f;
    public float fireRate = 0.1f; // higher == faster
    private float fireCooldown = 0f;
    public int damage;
    public float cylActivationDelay = 5f; // Delay before cyl becomes active
    public float cylActiveDuration = 2f;  // How long the cyl stays active

    private bool isCylActive = false;
    private bool cantRotate = false;

    void Start()
    {
        cyl.SetActive(false);
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f); // call UpdateTarget 2x/s
    }

    void Update()
    {
        if (target == null)
        {
            return;  // Stop flash & reset when current target dies or is out of range
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

    // Rotate towards the current target
    void RotateTowardsTarget()
    {
        if(!cantRotate)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = lookRotation.eulerAngles;
            rotatePart.rotation = Quaternion.Euler(0f, rotation.y - 180, 0f);
        }

    }

    // Fire the weapon
    void Shoot()
    {
        flash.Play();
        StartCoroutine(ActivateCylAfterDelay());
    }

    // Coroutine to activate the cyl after a delay
    IEnumerator ActivateCylAfterDelay()
    {
        yield return new WaitForSeconds(cylActivationDelay); // Wait before activating cyl
        cyl.SetActive(true);
        isCylActive = true;
        cantRotate = true;

        yield return new WaitForSeconds(cylActiveDuration); // Wait while cyl is active
        cyl.SetActive(false);
        isCylActive = false;
        cantRotate = false;
    }

    // Handle damage when enemies pass through the cyl
    void OnTriggerEnter(Collider other)
    {
        if (isCylActive && other.CompareTag(targetTag))
        {
            Debug.Log("Hit enemy: " + other.name); // Add debugging to check if enemies are detected
            Damage(other.transform);
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            Debug.Log("Applying damage to: " + enemy.name); // Debug log to verify damage application
            e.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("Enemy component not found on: " + enemy.name);
        }
    }

    // turret range viz onclick
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}