using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChromeCollisionImpact : PoolableObject
{
    ParticleSystem ps;
    AudioSource audioSource;
    public float duration;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SpawnImpact()
    {
        ps.Play();
        audioSource.Play();
        StartCoroutine(DurationCoroutine());
    }

    private IEnumerator DurationCoroutine()
    {
        yield return (duration);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
    }
}
