using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range = 5f;
    public string tag = "Enemy";
	public Transform rotatePart;

    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // call update target 2x/a
    }

    void UpdateTarget ()
	{
		//calc closest enemy within range
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
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

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			//targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else
		{
			target = null;
		}

	}

    // Update is called once per frame
    void Update()
    {
        if(target == null)
		{
			return; //reset when curr target dies/outofrange
		}
		//rotate turret to face current target
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		//Vector3 rotation = lookRotation.eulerAngles;
        Vector3 rotation = Quaternion.Lerp(rotatePart.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        rotatePart.rotation = Quaternion.Euler(0f, rotation.y -180, 0f);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); //range viz
    }
}
