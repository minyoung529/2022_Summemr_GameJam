using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField, Header("MAX 타워 용량")]
    float maxTowerGage;
    [SerializeField, Header("처음 타워 용량")]
    float curTowerGage;

    [SerializeField, Header("용량 차는 속도")]
    float fullSpeed;

    // 게이지바
    [SerializeField, Header("아빠 타워 용량 게이지바")]
    Image dadGageImage;
    [SerializeField, Header("오빠 타워 용량 게이지바")]
    Image brotherGageImage;

    // 현재 게이지
    float dadTowerGage;
    float brotherTowerGage;


    private void ResetGame()
    {
        // 타워 리셋
        dadTowerGage = curTowerGage;
        brotherTowerGage = curTowerGage;
    }

    private void Update()
    {
        // 게임 오버 조건
        if(dadTowerGage == maxTowerGage || brotherTowerGage == maxTowerGage)
        {
            // 게임 오버
            Debug.Log("Over");
        }
        CheckGage();
    }

    void CheckGage()
    {
        brotherGageImage.fillAmount = Mathf.Lerp(brotherGageImage.fillAmount, brotherTowerGage / maxTowerGage, Time.deltaTime * fullSpeed);
        dadGageImage.fillAmount = Mathf.Lerp(dadGageImage.fillAmount, dadTowerGage / maxTowerGage, Time.deltaTime * fullSpeed);
    }

    public void TestGage()
    {
        dadTowerGage += 10;
        brotherTowerGage += 10;
    }
}
