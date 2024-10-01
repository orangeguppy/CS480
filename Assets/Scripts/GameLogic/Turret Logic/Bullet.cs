using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 25f;

    [Header("Missiles")]
    public bool isMissile;
    public GameObject onHitEffect;
    public float explosionRadius = 1f;


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
        transform.LookAt(target);
    }

    void DamageTarget()
    {
        
        if (isMissile)
        {
            Debug.Log("boom");
            GameObject effect = (GameObject)Instantiate(onHitEffect, transform.position, transform.rotation);
            Destroy(effect, 2f);
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("ow");
            Damage(target);
        }
        
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        //e.TakeDamage(damage);
    }


    void OnDrawGizmosSelected() // explosion radius check
    {
        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
