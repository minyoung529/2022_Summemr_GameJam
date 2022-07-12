using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tset : MonoBehaviour
{
    public HoleScript hole;

    [ContextMenu("enable")]
    public void EnableHole()
    {
        hole.EnableHole();
    }

    [ContextMenu("disable")]
    public void DisableHole()
    {
        hole.DisableHole();
    }
    
}
