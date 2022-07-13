using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChromeIcon : ProgramIcon
{
    [SerializeField] private Chrome[] chrome;

    [SerializeField] private Image[] reds;
    [SerializeField] private Image[] yellows;
    [SerializeField] private Image[] greens;
    [SerializeField] private Image[] circles;

    [SerializeField] private Sprite[] redSprites;
    [SerializeField] private Sprite[] yellowSprites;
    [SerializeField] private Sprite[] greenSprites;
    [SerializeField] private Sprite[] circleSprites;

    [SerializeField] private Sprite[] chromes;

    protected override void ExecuteProgram()
    {
        SoundManager.Instance.TabOpen();
        OnCoolTime();
        //OnCoolTime();
        for(int i = 0; i < level; i++)
        {
            chrome[i].EnableChrome();
        }
    }

    protected override void ChildLevelUp()
    {
        foreach(Image image in reds)
        {
            image.sprite = redSprites[level - 1];
        }

        foreach (Image image in yellows)
        {
            image.sprite = yellowSprites[level - 1];
        }

        foreach (Image image in greens)
        {
            image.sprite = greenSprites[level - 1];
        }

        foreach (Image image in circles)
        {
            image.sprite = circleSprites[level - 1];
        }
    }
}
