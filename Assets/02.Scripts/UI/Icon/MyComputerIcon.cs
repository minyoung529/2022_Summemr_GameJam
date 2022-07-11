using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComputerIcon : ProgramIcon
{
    [SerializeField]
    GameObject myComputer;
    protected override void ExecuteProgram()
    {
        myComputer.SetActive(true);
    }
}
