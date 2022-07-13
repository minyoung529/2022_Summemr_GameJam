using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    [SerializeField, Header("MAX 타워 용량")]
    float maxTowerGage;
    [SerializeField, Header("용량 차는 속도")]
    float fullSpeed;

    [SerializeField]
    MeshRenderer overMesh;
    [SerializeField]
    GameObject brokenImage;

    // 게이지바
    [SerializeField, Header("아빠 타워 용량 게이지바")]
    Image dadGageImage;
    [SerializeField, Header("오빠 타워 용량 게이지바")]
    Image brotherGageImage;
    
    [SerializeField]
    Text broPerText;
    [SerializeField]
    Text dadPerText;

    bool isBroken = false;

    private void Awake()
    {
        ResetGame();
    }

    private void Update()
    {
        if (GameManager.Instance.dadTowerGage >= maxTowerGage && GameManager.Instance.brotherTowerGage >= maxTowerGage)
        {
            PlayerPrefs.SetInt("SCORE", GameManager.Instance.score);
            UIManager.Instance.SceneChange("Over");
        }
        else if (GameManager.Instance.dadTowerGage >= maxTowerGage/2 && GameManager.Instance.brotherTowerGage >= maxTowerGage/2)
        {
            // 경고
            if (isBroken) return;
            SoundManager.Instance.SfxSoundOn(10);
            isBroken = true;
            brokenImage.SetActive(true);
            CameraShake.Instance.Shake();
        }
       
        CheckGage();

    }

    private void ResetGame()
    {
        // 타워 리셋
        brotherGageImage.fillAmount = 0;
        dadGageImage.fillAmount = 0;

    }
    void CheckGage()
    {
        dadPerText.text = string.Format($"{GameManager.Instance.dadTowerGage}%");
        broPerText.text = string.Format($"{GameManager.Instance.brotherTowerGage}%");
        brotherGageImage.fillAmount = Mathf.Lerp(brotherGageImage.fillAmount, GameManager.Instance.brotherTowerGage / maxTowerGage, Time.deltaTime * fullSpeed);
        dadGageImage.fillAmount = Mathf.Lerp(dadGageImage.fillAmount, GameManager.Instance.dadTowerGage / maxTowerGage, Time.deltaTime * fullSpeed);
    }

}
