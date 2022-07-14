using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoticePanel : MonoBehaviour
{
    private Image image;
    private Color originalColor;

    public Image iconImage;
    public Text noticeText;
    public bool canUse = true;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void SetInfo(Sprite sprite, string text)
    {
        gameObject.SetActive(true);
        image.color = Color.clear;
        image.DOColor(originalColor, 0.5f);

        iconImage.sprite = sprite;

        noticeText.text = $"{text}\n지금 바로 업그레이드 가능!";
        canUse = false;

        StartCoroutine(DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(3f);

        image.DOColor(originalColor, 0.5f).OnComplete(() =>
        {
            image.DOKill();
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
        canUse = true;
    }
}