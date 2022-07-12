using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monster : PoolableObject
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private ParticleSystem dieEffect;
    public MonsterSpawner spawner;
    private Rigidbody rigid;
    private Transform target;

    private bool isVaccine = false;
    public bool IsVaccine
    {
        get => isVaccine;
        set
        {
            if (value)
            {
                ChangeToVaccine();
            }

            isVaccine = value;
        }
    }

    private MeshRenderer meshRenderer;
    public Material[] materials;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 백신 상태인 다른 바이러스와 충돌했을때
    /// </summary>
    public void VaccineCollisionEnter()
    {
        ChangeToVaccine();
    }

    private void Update()
    {
        if (isVaccine)
        {
            VaccineMove();
        }
        else
        {
            MoveToTarget();
        }

        if (transform.position.y < -20f)
        {
            DieMonster();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Tower"))
        {
            DieMonster();
        }
        //if(other.transform.CompareTag("Hole"))
        //{
        //    SetTarget(other.transform);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Hole"))
        {
            SetTarget(spawner.Tower);
        }
    }

    public void Die()
    {
        //죽는 연출
        dieEffect.Play();
        SoundManager.Instance.SfxSoundOn(1);
        Invoke("DieMonster", 0.05f);
    }

    public void DieMonster()
    {
        PoolManager.Instance.Push(this);
    }

    private void MoveToTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized * speed;
        dir.y = rigid.velocity.y;
        rigid.velocity = dir;
    }

    private void VaccineMove()
    {
        Vector3 dir = -(target.position - transform.position).normalized * speed;
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

    public void ChangeToVaccine()
    {
        isVaccine = true;
        meshRenderer.material = materials[1];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            Debug.Log("sdf");
            collision.transform.GetComponent<Monster>().ChangeToVaccine();
        }
    }

    public override void Reset()
    {
        StopAllCoroutines();
        meshRenderer.material = materials[0];

        rigid.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        //isVaccine = false;
    }
}
