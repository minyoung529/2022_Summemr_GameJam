using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintIcon : ProgramIcon
{
    [SerializeField]
    GameObject paintUI;
    [SerializeField]
    GameObject paint;
    protected override void ExecuteProgram()
    {
        paintUI.SetActive(true);
        paint.SetActive(true);
    }
}
