using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PaintIcon : ProgramIcon
{
    [SerializeField]
    GameObject paintUI;
    [SerializeField]
    GameObject paint;
    [SerializeField]
    private float gagueUpgradeFactor = 2f;

    private DrawLine paintCompo = null;

    protected override void Awake()
    {
        base.Awake();
        try
        {
            paintCompo = paint.transform.Find("MSPaint (1)").GetComponent<DrawLine>();
        }
        catch(NullReferenceException ne)
        {
            Debug.LogError("그림판 컴포넌트를 찾을 수 없습니다. 경로를 확인해주세요");
        }
    }


    protected override void ExecuteProgram()
    {
        SoundManager.Instance.ProgramOpen();

        UIManager.Instance.ActiveWindow(paint);
        paintUI.SetActive(true);

        paint.transform.localScale = Vector3.zero;
        paint.SetActive(true);
        paint.transform.DOScale(1f, 0.3f);
    }

    protected override void ChildLevelUp()
    {
        paintCompo.UpgradeMaxGague(gagueUpgradeFactor);
    }
}
