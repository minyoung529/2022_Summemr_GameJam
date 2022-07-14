using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monster : PoolableObject
{
    [SerializeField] private MonsterType type;
    [SerializeField] private float speed = 10f;
    [SerializeField] private ParticleSystem[] dieEffect;
    [SerializeField] private ParticleSystem damageEffect;
    public MonsterSpawner spawner;
    private Rigidbody rigid;
    private Transform target;
    private int heart = 1;
    public int attackPower = 10;

    //float minSize = 0.04f;
    //float maxSize = 0.05f;

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

    bool isDie = false;

    private MeshRenderer meshRenderer;
    public Material[] materials;

    new private Collider collider;
    private Collider vaccineCollider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        vaccineCollider = transform.GetChild(0).GetComponent<Collider>();
        if (type == MonsterType.SLOW)
        {
            attackPower = 20;
            heart = 4;
            //minSize = 0.09f;
            //maxSize = 0.1f;
        }
        //else if (type == MonsterType.FAST)
        //{
        //    minSize = 0.03f;
        //    maxSize = 0.04f;
        //}
    }

    /// <summary>
    /// ���?������ �ٸ� ���̷����� �浹������
    /// </summary>
    public void VaccineCollisionEnter()
    {
        ChangeToVaccine();
    }

    private void OnEnable()
    {
        ResetSprite();
        collider.enabled = true;
        rigid.useGravity = true;
    }
    private void Update()
    {
        if (isVaccine)
        {
            if (isDie) return;
            VaccineMove();
        }
        else
        {
            if (isDie) return;
            MoveToTarget();
        }

        if (transform.position.y < -20f)
        {
            GameManager.Instance.gold += 100;
            DieMonster();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Chrome"))
        {
            Damaged();
        }
        if (other.transform.CompareTag("Hole"))
        {
            SetTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Hole"))
        {
            //SetTarget(spawner.Tower);
        }
    }
    private void DieSprite()
    {
        meshRenderer.material = materials[2];
    }

    private void ResetSprite()
    {
        isDie = false;
        meshRenderer.material = materials[0];
    }

    public void Damaged()
    {
        heart--;
        if (heart <= 0)
        {
            Die();
        }
        else
        {
            SoundManager.Instance.MonsterDamageSound();
            damageEffect.Play();
        }
    }

    public void Die()
    {
        //�״� ����

        GameManager.Instance.gold += 100;

        collider.enabled = false;
        vaccineCollider.enabled = false;
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        isDie = true;
        DieSprite();

        foreach (ParticleSystem p in dieEffect)
        {
            p.Play();
        }

        SoundManager.Instance.MonsterDieSound();
        Invoke("DieMonster", 1f);
    }

    public void DieMonster()
    {
        PoolManager.Instance.Push(this);
    }

    public void DieMonsterByTower()
    {
        if (IsVaccine) return;
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

    public void ChangeToVaccine()
    {
        isVaccine = true;
        meshRenderer.material = materials[1];
    }

    public override void Reset()
    {
        StopAllCoroutines();
        meshRenderer.material = materials[0];

        rigid.useGravity = true;
        collider.enabled = true;
        vaccineCollider.enabled = true;

        rigid.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        GameManager.Instance.monsters.Remove(this);
        isVaccine = false;
    }
}
