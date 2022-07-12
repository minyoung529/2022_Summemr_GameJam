using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComputerIcon : ProgramIcon
{
    [SerializeField]
    GameObject myComputer;
    [SerializeField]
    GameObject gageObj;

    protected override void ExecuteProgram()
    {
        myComputer.SetActive(true);
        gageObj.SetActive(true);
    }
}
