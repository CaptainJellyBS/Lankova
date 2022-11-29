using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float moveSpeed, flightTime = 3.0f, damage = 5.0f;
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(TimeOut());
        rb.velocity = transform.forward * moveSpeed;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        collision.collider.GetComponentInParent<IDamagable>()?.TakeDamage(damage, collision.collider);
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        if(ps != null) { ps.transform.parent = null; Destroy(ps.gameObject, 1.0f); }
        Destroy(gameObject);        
    }

    protected virtual IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(flightTime);
        OnTimeOut();
    }

    protected virtual void OnTimeOut()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        if (ps != null) { ps.transform.parent = null; Destroy(ps.gameObject, 1.0f); }

        
        Destroy(gameObject);        
    }
}
