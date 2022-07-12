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
    [SerializeField]
    GameObject coolImage;

    protected override void ExecuteProgram()
    {
        coolImage.SetActive(true);
        OnCoolTime(coolImage);
        paintUI.SetActive(true);
        paint.SetActive(true);
    }
}
