using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MailIcon : ProgramIcon
{
    [SerializeField] private Transform windowImage;

    protected override void ExecuteProgram()
    {
        if (windowImage.gameObject.activeSelf) return;

        windowImage.gameObject.SetActive(true);
        windowImage.transform.localScale = Vector3.zero;
        windowImage.transform.DOScale(1.2f, 0.3f);
        OnCoolTime();
    }
}
