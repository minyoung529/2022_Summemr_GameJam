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

    private void Awake()
    {
        ResetGame();
    }
    private void Update()
    {
        // 게임 오버 조건
        dadTowerGage = GameManager.Instance.dadTowerGage;
        brotherTowerGage = GameManager.Instance.brotherTowerGage;
        if (dadTowerGage == maxTowerGage || brotherTowerGage == maxTowerGage)
        {
            // 게임 오버
            Debug.Log("Over");
        }
    }

    private void ResetGame()
    {
        // 타워 리셋
        dadTowerGage = curTowerGage;
        brotherTowerGage = curTowerGage;
        brotherGageImage.fillAmount = brotherTowerGage / curTowerGage;
        dadGageImage.fillAmount = dadTowerGage / curTowerGage;
    }
    void CheckGage()
    {
        brotherGageImage.fillAmount = Mathf.Lerp(brotherGageImage.fillAmount, brotherTowerGage / maxTowerGage, Time.deltaTime * fullSpeed);
        dadGageImage.fillAmount = Mathf.Lerp(dadGageImage.fillAmount, dadTowerGage / maxTowerGage, Time.deltaTime * fullSpeed);
    }

    private void OnEnable()
    {
        InvokeRepeating("CheckGage", 0f, 1f);
    }
    private void OnDisable()
    {
        CancelInvoke("CheckGage");
    }
}
