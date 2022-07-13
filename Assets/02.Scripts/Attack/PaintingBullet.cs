using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingBullet : PoolableObject
{
    private Rigidbody rigid;
    private MeshRenderer meshRenderer;
    private float scale;

    private void OnEnable()
    {
        meshRenderer ??= GetComponent<MeshRenderer>();

        float offset = 0.35f;
        int gridCount = 3;
        float x = Random.Range(0, gridCount) * offset;
        float y = Random.Range(0, gridCount) * offset;

        meshRenderer.material.mainTextureOffset = new Vector2(x, y);

        transform.localScale = Vector3.one * scale;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        meshRenderer ??= GetComponent<MeshRenderer>();

        scale = transform.localScale.x;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.z) > 10f)
        {
            if (gameObject.activeSelf)
                PoolManager.Instance.Push(this);
        }
    }

    public void SetDirection(Vector3 velocity)
    {
        rigid.AddForce(velocity * 600f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Monster"))
        {
            other.transform.GetComponent<Monster>().Damaged();
            //PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {
        rigid.velocity = Vector3.zero;
    }
}