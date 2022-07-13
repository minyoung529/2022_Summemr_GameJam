using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailScript : PoolableObject
{
    private Image[] images;
    public float duration;
    public Color trailColor;

    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
    }

    public void SpawnTrail(float alpha)
    {
        SetAlpha(alpha);
        StartCoroutine(DurtaionCoroutine());
    }

    private void SetAlpha(float alpha)
    {
        foreach(Image image in images)
        {
            image.color = new Color(trailColor.r, trailColor.g, trailColor.b, alpha);
        }
    }

    private IEnumerator DurtaionCoroutine()
    {
        float time = 0f;
        float alpha = images[0].color.a;
        while (time <= duration)
        {
            SetAlpha(alpha * (duration - time));
            time += Time.deltaTime;
            yield return null;
        }
        transform.SetParent(PoolManager.Instance.transform);
        PoolManager.Instance.Push(this);
    }

    public void SetSprite(Sprite center, Sprite g, Sprite r, Sprite y)
    {
        images[0].sprite = center;
        images[1].sprite = g;
        images[2].sprite = r;
        images[3].sprite = y;
    }

    public override void Reset()
    {
    }
}
