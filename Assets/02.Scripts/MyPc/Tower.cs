using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField, Header("MAX Ÿ�� �뷮")]
    float maxTowerGage;
    [SerializeField, Header("ó�� Ÿ�� �뷮")]
    float curTowerGage;

    [SerializeField, Header("�뷮 ���� �ӵ�")]
    float fullSpeed;

    // ��������
    [SerializeField, Header("�ƺ� Ÿ�� �뷮 ��������")]
    Image dadGageImage;
    [SerializeField, Header("���� Ÿ�� �뷮 ��������")]
    Image brotherGageImage;

    // ���� ������
    float dadTowerGage;
    float brotherTowerGage;

    private void Awake()
    {
        ResetGame();
    }
    private void Update()
    {
        // ���� ���� ����
        dadTowerGage = GameManager.Instance.dadTowerGage;
        brotherTowerGage = GameManager.Instance.brotherTowerGage;
        if (dadTowerGage == maxTowerGage || brotherTowerGage == maxTowerGage)
        {
            // ���� ����
            Debug.Log("Over");
        }
    }

    private void ResetGame()
    {
        // Ÿ�� ����
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
