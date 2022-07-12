using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZoomEffect : MonoBehaviour
{
    [SerializeField] private float endValue = 1.1f;

    void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(endValue, 0.7f));
        seq.Append(transform.DOScale(1f, 0.4f));

        seq.SetLoops(-1,LoopType.Restart);
    }
}
