using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 75f;

    public void Hit (Transform _target)
    {
        target = _target;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float dist = speed * Time.deltaTime;

        if(dir.magnitude <= dist )
        {
            DamageTarget();
            return;
        }

        transform.Translate(dir.normalized * dist, Space.World);
    }

    void DamageTarget()
    {
        Debug.Log("ow");
        Destroy(gameObject);
    }
}
