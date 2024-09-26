using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum PathType { PathOne, PathTwo, PathThree, PathFour }
    public PathType pathType = PathType.PathOne; // choose the path type from Inspector

    public float rotationSpeed = 25f;
    private Transform target;
    private int waypointIdx = 0;

    [Header("Enemy Stats")]
    public float speed;
    public int health;

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
        } else if (pathType == PathType.PathFour)
        {
            target = EnemyPathFour.waypoints[0];
        }
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Helper method to get the current path waypoints
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
