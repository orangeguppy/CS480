using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMvmtOne : MonoBehaviour
{
    public float speed;
    private Transform target;
    private int waypointIdx = 0;

    void Start()
    {
        target = EnemyPathOne.waypoints[0];
    }

    // move btw waypoints;
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIdx >= EnemyPathOne.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIdx++;
        target = EnemyPathOne.waypoints[waypointIdx];
    }
}
