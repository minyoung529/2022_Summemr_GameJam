using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void ExplosionDamage(Vector3 explosionPos, float force = 1f)
    {
        Debug.Log("Damaged");
        Vector3 direction = (transform.position - explosionPos).normalized;
        direction.y = 0f;

        Vector3 targetPos = transform.position + direction * force;
        rigid.DOMove(targetPos, 1f);
    }
}
