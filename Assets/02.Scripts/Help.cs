using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Help : MonoBehaviour
{
    private Image image;
    private Text text;

    void Start()
    {
        transform.localScale = Vector3.one;
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2.5f);
        seq.Append(image.DOFade(0f, 3f));
        seq.Join(text.DOFade(0f, 3f));
        seq.AppendCallback(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        image.DOKill();
    }
}
