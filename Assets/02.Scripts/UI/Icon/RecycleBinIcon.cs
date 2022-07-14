using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBinIcon : ProgramIcon
{
    public HoleScript holeScript;
    [SerializeField] private float duration;
    [SerializeField] private float cooldown;
    private bool isCooldown;
    protected HoleScript hole;

    protected override void ExecuteProgram()
    {
        if(!isCooldown)
        {
            SoundManager.Instance.ProgramOpen();
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

    protected override void ChildLevelUp()
    {
        hole ??= FindObjectOfType<HoleScript>();

        if (level == 2)
        {
            hole.HoleSize = 1.4f;
        }
        else
        {
            hole.HoleSize = 2f;
        }
    }
}
