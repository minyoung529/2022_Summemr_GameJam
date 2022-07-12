using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VirusObject : PoolableObject
{
    private Monster targetMonster;

    public float speed = 6f;

    private void OnEnable()
    {
        transform.position = Camera.main.transform.position;
    }

    public void SetTarget(Monster monster)
    {
        targetMonster = monster;
    }

    private void Update()
    {
        if (targetMonster)
            transform.position = Vector3.MoveTowards(transform.position, targetMonster.transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == targetMonster.transform)
        {
            gameObject.SetActive(false);
            targetMonster.IsVaccine = true;
            PoolManager.Instance.Pop(this);
        }
    }

    public override void Reset()
    {

    }
}
