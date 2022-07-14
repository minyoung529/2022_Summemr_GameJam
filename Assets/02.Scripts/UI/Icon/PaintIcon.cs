using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PaintIcon : ProgramIcon
{
    [SerializeField]
    GameObject paintUI;
    [SerializeField]
    GameObject paint;

    public DrawLine drawLine;


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
        if (level == 2)
        {
            drawLine.bulletCount = 20;
        }
        else
        {
            drawLine.bulletCount = 30;
        }

        drawLine.level = level;
    }
}
