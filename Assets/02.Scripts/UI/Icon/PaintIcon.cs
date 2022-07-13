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
    

    protected override void ExecuteProgram()
    {
        SoundManager.Instance.ProgramOpen();
        OnCoolTime();
        paintUI.SetActive(true);
        paint.SetActive(true);
    }
}
