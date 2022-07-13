using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingBarMove : MonoBehaviour
{
    private RectTransform parent;
    private RectTransform rect;
    private Sequence seq;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        //parent = GetComponentInParent<RectTransform>();
        parent = transform.parent.gameObject.GetComponent<RectTransform>();
        seq = DOTween.Sequence();
    }

    private void Start()
    {
        seq.Append(rect.DOAnchorPosX(rect.anchoredPosition.x + parent.rect.width + rect.rect.width, 2.5f).SetEase(Ease.Linear));
        seq.SetLoops(-1, LoopType.Restart);
    }

    private void OnDisable()
    {
        seq.Kill();
    }
}
