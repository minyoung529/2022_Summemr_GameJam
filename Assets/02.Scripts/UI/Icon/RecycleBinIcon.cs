using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBinIcon : ProgramIcon
{
    public OnChangePosition holeScript;
    [SerializeField] private float duration;
    [SerializeField] private float cooldown;
    private bool isCooldown;

    protected override void ExecuteProgram()
    {
        if(!isCooldown)
        {
            StartCoroutine(CooldownCoroutine());
            holeScript.EnableHole();
            StartCoroutine(DurationCoroutine());
        }
    }

    private IEnumerator DurationCoroutine()
    {
        yield return new WaitForSeconds(duration);
        holeScript.DisableHole();
    }

    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(duration);
        isCooldown = false;
    }
}
