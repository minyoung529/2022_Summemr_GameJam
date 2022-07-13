using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartAttack : PoolableObject
{
    private Rigidbody rigid;
    private bool isExplosion = false;

    [SerializeField] private float gravity;

    private int count = 0;

    Camera mainCam;
    Vector3 cameraPos;

    void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody>();
        mainCam = Camera.main;
        cameraPos = mainCam.transform.position;
        AppearHeart();
    }

    private void Start()
    {
        EmojiCollider obj = GetComponentInChildren<EmojiCollider>();
        obj.AddPlatformAction(Destroy);
        obj.AddMonsterAction(Attack);
    }

    private void OnEnable()
    {
        rigid.transform.localPosition = Vector3.zero;
        rigid.WakeUp();
    }

    void Update()
    {
        Gravity();
    }

    public void AppearHeart()
    {
        rigid.velocity = Vector3.zero;

        Vector3 direction = Vector3.zero;
        direction.x = Random.Range(-1f, 1f);
        direction.z = Random.Range(0f, 1f);

        rigid.AddForce(direction * 700);
    }

    private void Gravity()
    {
        rigid.AddForce(Vector3.back * gravity);
    }

    public void Explosion(Vector3 explosionPosition, float radius)
    {
        rigid.AddExplosionForce(1800f, explosionPosition, radius);
        isExplosion = true;

        if (gameObject.activeSelf)
            StartCoroutine(DestroyCoroutine());
    }

    private void Destroy()
    {
        if (isExplosion && ++count > 1)
        {
            PoolManager.Instance.Push(this);
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(5f);

        if (gameObject.activeSelf && isExplosion)
        {
            transform.position = Vector3.zero;
            PoolManager.Instance.Push(this);
        }
    }

    private void Attack(Collision collision)
    {
        collision.transform.GetComponent<Monster>().Die();
        Destroy();
    }

    public override void Reset()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        rigid.Sleep();

        isExplosion = false;
        count = 0;
    }
}