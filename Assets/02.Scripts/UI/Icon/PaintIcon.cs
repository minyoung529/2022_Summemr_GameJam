using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintIcon : ProgramIcon
{
    [SerializeField]
    GameObject paint;
    protected override void ExecuteProgram()
    {
        paint.SetActive(true);
    }
}
