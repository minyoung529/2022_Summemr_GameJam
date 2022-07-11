using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmojiCollider : MonoBehaviour
{
    private Action onEnterPlatform;
    public LayerMask layerMask;

    public void AddAction(Action action)
    {
        onEnterPlatform += action;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Plane"))
        {
            Debug.Log("Invoke");
            onEnterPlatform.Invoke();
        }
    }
}
