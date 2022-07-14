using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingBullet : PoolableObject
{
    public PaintIcon _icon;
    private Rigidbody rigid;
    private MeshRenderer meshRenderer;
    private float scale;

    public Material[] colors;

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

    public void ChangeColor(int grade)
    {
        switch (grade)
        {
            case 1:
                meshRenderer.material = colors[0];
                break;

            case 2:
                meshRenderer.material = colors[1];
                break;

            case 3:
                meshRenderer.material = colors[Random.Range(1, colors.Length)];
                break;
        }

        float offset = 0.35f;
        float x = Random.Range(0, 3) * offset;
        float y = Random.Range(0, 3) * offset;

        meshRenderer.material.mainTextureOffset = new Vector2(x, y);
        transform.localScale = Vector3.one * scale;
    }

    public void SetDirection(Vector3 dir, float range)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transform.position + dir * range, 1f*_icon.level));
        seq.AppendCallback(() => PoolManager.Instance.Push(this));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Monster"))
        {
            other.transform.GetComponent<Monster>().Damaged();
            PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {
        rigid.velocity = Vector3.zero;
    }
}