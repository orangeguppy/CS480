using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Targeting")]
    public string targetTag = "Enemy";
    private Transform target;

    [Header("Turret Parts")]
    public Transform rotatePart;
    public Transform bulletPrefab;
    public Transform firingPoint;
    public GameObject flash;

    [Header("Turret Stats")]
    public float range = 5f;    
    public float fireRate = 1f; // higher == faster
    private float fireCooldown = 0f;
    public int damage;

    private Coroutine flashCoroutine;

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f); // call UpdateTarget 2x/s
    }

    void Update()
    {
        if (target == null)
        {
            StopFlashEffect();
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
        ActivateFlashEffect();
        //Destroy(target.gameObject);
        GameObject bulletObject = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation).gameObject;
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Hit(target);
            //bullet.SetDamage(damage);
        }
    }

    void ActivateFlashEffect()
    {
        flash.SetActive(true);

        // reset the flash effect coroutine if alr active
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(DisableFlash());
    }

    IEnumerator DisableFlash()
    {
        yield return new WaitForSeconds(2f);
        flash.SetActive(false);
    }

    void StopFlashEffect()
    {
        flash.SetActive(false);

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }
    }

    // turret range viz onclick
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
