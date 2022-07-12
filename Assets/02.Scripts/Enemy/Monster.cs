using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : PoolableObject
{
    [SerializeField] private float speed = 10f;
    private Rigidbody rigid;
    private Transform target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveToTarget();
        if (transform.position.y < -20f)
        {
            Die();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Tower"))
        {
            Die();
        }
    }

    public void Die()
    {
        //Á×´Â ¿¬Ãâ
        PoolManager.Instance.Push(this);
    }

    private void MoveToTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized * speed;
        dir.y = rigid.velocity.y;
        rigid.velocity = dir;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ExplosionDamage(Vector3 explosionPos, float force = 1f)
    {
        rigid.AddExplosionForce(force, explosionPos, force);
    }

    public override void Reset()
    {
        StopAllCoroutines();
        rigid.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
