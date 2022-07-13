using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MailIcon : ProgramIcon
{
    [SerializeField] private Transform windowImage;
    [SerializeField] private Mail mail;
    [SerializeField] private Transform targetPicker;

    protected override void ExecuteProgram()
    {
        if (windowImage.gameObject.activeSelf) return;

        windowImage.gameObject.SetActive(true);
        windowImage.transform.localScale = Vector3.zero;
        windowImage.transform.DOScale(1.2f, 0.3f);
        OnCoolTime();
    }

    protected override void ChildLevelUp()
    {
        mail.distance += 0.6f;

        Vector3 scale = targetPicker.localScale;
        scale.x += 20f;
        scale.y += 10f;
        scale.z =1f;
        targetPicker.transform.localScale = scale;
    }
}
