using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChromeIcon : ProgramIcon
{
    [SerializeField]
    GameObject coolImage;
    protected override void ExecuteProgram()
    {
        coolImage.SetActive(true);
        OnCoolTime(coolImage);
    }
}
