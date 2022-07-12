using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MailIcon : ProgramIcon
{
<<<<<<< HEAD
    [SerializeField] private Transform windowImage;

    protected override void ExecuteProgram()
    {
        if (windowImage.gameObject.activeSelf) return;

        windowImage.gameObject.SetActive(true);
        windowImage.transform.localScale = Vector3.zero;
        windowImage.transform.DOScale(1f, 0.3f);
=======
    [SerializeField]
    GameObject coolImage;
    protected override void ExecuteProgram()
    {
        coolImage.SetActive(true);
        OnCoolTime(coolImage);
>>>>>>> september
    }
}
