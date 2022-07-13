using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailSpawner : MonoBehaviour
{
    [SerializeField] private TrailScript prefab;
    [SerializeField] private float startAlpha;
    [SerializeField] private float delay;

    [SerializeField] private Image center;
    [SerializeField] private Image g;
    [SerializeField] private Image r;
    [SerializeField] private Image y;


    public void EnableSpawn()
    {
        StartCoroutine(MakeTrails());
    }
    public void DisableSpawn()
    {
        StopAllCoroutines();
    }

    IEnumerator MakeTrails()
    {
        while (true)
        {
            TrailScript obj = PoolManager.Instance.Pop(prefab) as TrailScript;
            obj.transform.SetParent(transform.parent);
            obj.transform.localScale = Vector3.one;
            obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
            obj.gameObject.SetActive(true);
            obj.SpawnTrail(startAlpha);
            obj.SetSprite(center.sprite, g.sprite, r.sprite, y.sprite);
            yield return new WaitForSeconds(delay);
        }
    }
}
