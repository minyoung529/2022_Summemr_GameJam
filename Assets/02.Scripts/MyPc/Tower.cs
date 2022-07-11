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


    private void ResetGame()
    {
        // Ÿ�� ����
        dadTowerGage = curTowerGage;
        brotherTowerGage = curTowerGage;
    }

    private void Update()
    {
        // ���� ���� ����
        if(dadTowerGage == maxTowerGage || brotherTowerGage == maxTowerGage)
        {
            // ���� ����
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
