using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;

public class InstagramIcon : ProgramIcon
{
    [SerializeField] private Transform windowImage;

    public HeartButton heartButton;
    public VideoPlayer videPlayer;

    protected override void ExecuteProgram()
    {
        if (windowImage.gameObject.activeSelf) return;
        SoundManager.Instance.ProgramOpen();
        UIManager.Instance.ActiveWindow(windowImage.gameObject);
        windowImage.gameObject.SetActive(true);
        windowImage.transform.localScale = Vector3.zero;

        windowImage.transform.DOKill();
        windowImage.transform.DOScale(1f, 0.3f);

        videPlayer.time = 0f;
    }

    protected override void ChildLevelUp()
    {
        base.ChildLevelUp();
    }
}