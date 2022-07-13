using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePanel : MonoBehaviour
{
    public Image iconImage;
    public Text noticeText;
    public bool canUse = true;

    public void SetInfo(Sprite sprite, string text)
    {
        gameObject.SetActive(true);
        iconImage.sprite = sprite;
        noticeText.text = text;
        canUse = false;
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        canUse = true;
    }
}