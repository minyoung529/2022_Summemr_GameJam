using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmojiCollider : MonoBehaviour
{
    private Action onEnterPlatform;
    private Action<Collision> onEnterMonster;

    public void AddPlatformAction(Action action)
    {
        onEnterPlatform += action;
    }

    public void AddMonsterAction(Action<Collision> action)
    {
        onEnterMonster += action;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            onEnterPlatform.Invoke();
        }

        if (collision.transform.CompareTag("Monster"))
        {
            Debug.Log("jhjh");
            onEnterMonster.Invoke(collision);
        }
    }
}
