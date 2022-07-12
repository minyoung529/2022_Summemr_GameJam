using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChromeIcon : ProgramIcon
{
    [SerializeField] private Chrome[] chrome;
    protected override void ExecuteProgram()
    {
<<<<<<< HEAD
        SoundManager.Instance.TabOpen();
        OnCoolTime();
=======
        //OnCoolTime();
        for(int i = 0; i < chrome.Length; i++)
        {
            chrome[i].EnableChrome();
        }
>>>>>>> Chrome
    }
}
