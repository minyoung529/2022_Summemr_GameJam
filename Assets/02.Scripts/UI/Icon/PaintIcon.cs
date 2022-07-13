using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintIcon : ProgramIcon
{
    [SerializeField]
    GameObject paintUI;
    [SerializeField]
    GameObject paint;

    public DrawLine drawLine;


    protected override void ExecuteProgram()
    {
        SoundManager.Instance.TabOpen();
        OnCoolTime();
        paintUI.SetActive(true);
        paint.SetActive(true);
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
    }
}
