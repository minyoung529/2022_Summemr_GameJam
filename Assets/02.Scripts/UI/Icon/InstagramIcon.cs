using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InstagramIcon : ProgramIcon
{
    [SerializeField] private Transform windowImage;

    protected override void ExecuteProgram()
    {
        if (windowImage.gameObject.activeSelf) return;
        SoundManager.Instance.ProgramOpen();
        OnCoolTime();
        windowImage.gameObject.SetActive(true);
        windowImage.transform.localScale = Vector3.zero;
        windowImage.transform.DOScale(1f, 0.3f);
    }
}
