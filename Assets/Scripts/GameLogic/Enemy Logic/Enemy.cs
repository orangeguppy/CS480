using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum PathType { PathOne, PathTwo, PathThree}
    [Header("Enemy Mvmt")]
    public PathType pathType = PathType.PathOne; // choose the path type from Inspector
    public Image healthBar;

    [Header("Enemy Mvmt")]
    private float rotationSpeed = 25f;
    private Transform target;
    private int waypointIdx = 0;

    [Header("Enemy Stats")]
    public float initSpeed;
    private float speed;
    private float health;
    public float initHealth;
    public int gold;
    public bool isCloaked;
    public bool onFire;
    public int score;
    public GameObject fireEffect;
    private Coroutine fireCoroutine;

    void Start()
    {
        // Select initial waypoint based on the chosen path
        if (pathType == PathType.PathOne)
        {
            target = EnemyPathOne.waypoints[0];
        }
        else if (pathType == PathType.PathTwo)
        {
            target = EnemyPathTwo.waypoints[0];
        } else if (pathType == PathType.PathThree)
        {
            target = EnemyPathThr.waypoints[0];
        }
        speed = initSpeed;
        health = initHealth;
    }

    // Move between waypoints
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        RotateTowardsTarget(dir);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            GetNextWaypoint();
        }

        speed = initSpeed;


        if (onFire && fireCoroutine == null)
        {
            StartBurning(); // Ensure burning starts if onFire is enabled during gameplay
        }
    }

    void RotateTowardsTarget(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion adjustedRotation = targetRotation * Quaternion.Euler(-90f, -90f, 0f); // manual adjustment
        transform.rotation = Quaternion.Slerp(transform.rotation, adjustedRotation, rotationSpeed * Time.deltaTime);
    }

    void GetNextWaypoint()
    {
        // Kill enemy at last waypoint
        if (waypointIdx >= GetCurrentPathWaypoints().Length - 1)
        {
            PlayerInfo.Lives--;
            Destroy(gameObject);
            return;
        }

        waypointIdx++;
        target = GetCurrentPathWaypoints()[waypointIdx];
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / initHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = initSpeed * (1f - amount);
    }

    public void SetOnFire(bool isBurning)
    {
        onFire = isBurning;

        if (onFire)
        {
            StartBurning();
        }
    }

    private void StartBurning()
    {
        fireCoroutine = StartCoroutine(BurningCoroutine());
        fireEffect.SetActive(true);
    }

    private IEnumerator BurningCoroutine()
    {
        while (onFire)
        {
            TakeDamage(0.2f); // Adjust the damage per second as needed
            yield return new WaitForSeconds(1f); // Wait for 1 second before applying the next damage
        }
    }

    void Die()
    {
        Destroy(gameObject);
        PlayerInfo.Money += gold;
        PlayerInfo.EndlessScore += score;

        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }


    Transform[] GetCurrentPathWaypoints()
    {
        if (pathType == PathType.PathOne)
        {
            return EnemyPathOne.waypoints;
        }
        else if (pathType == PathType.PathTwo)
        {
            return EnemyPathTwo.waypoints;
        }
        else if (pathType == PathType.PathThree)
        {
            return EnemyPathThr.waypoints;
        }
        else
        {
            return EnemyPathFour.waypoints;
        }
    }
}
