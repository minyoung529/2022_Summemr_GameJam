using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InstagramIcon : ProgramIcon
{
    [SerializeField] private Transform windowImage;

    public HeartButton heartButton;

    protected override void ExecuteProgram()
    {
        if (windowImage.gameObject.activeSelf) return;
        SoundManager.Instance.ProgramOpen();
        OnCoolTime();
        UIManager.Instance.ActiveWindow(windowImage.gameObject);
        windowImage.gameObject.SetActive(true);
        windowImage.transform.localScale = Vector3.zero;

        windowImage.transform.DOKill();
        windowImage.transform.DOScale(1f, 0.3f);
    }

    protected override void ChildLevelUp()
    {
        base.ChildLevelUp();
    }
}