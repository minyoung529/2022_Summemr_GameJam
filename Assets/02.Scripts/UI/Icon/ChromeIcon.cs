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

    protected override void ExecuteProgram()
    {
        Invoke("ChromeOnSound", 0.2f);
        for(int i = 0; i < level; i++)
        {
            chrome[i].EnableChrome();
        }
        Selecting.IsChrome = true;
    }

    void ChromeOnSound()
    {
        SoundManager.Instance.SfxSoundOn(12);
    }
    protected override void ChildLevelUp()
    {
        chrome[0].duration -= 0.35f;
        chrome[1].duration -= 0.35f;
        chrome[2].duration -= 0.35f;

        Debug.Log(chrome[0].duration);

        chrome[level - 1].gameObject.SetActive(true);
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
