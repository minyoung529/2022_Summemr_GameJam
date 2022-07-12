using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField, Header("MAX Ÿ�� �뷮")]
    float maxTowerGage;
    [SerializeField, Header("�뷮 ���� �ӵ�")]
    float fullSpeed;

    // ��������
    [SerializeField, Header("�ƺ� Ÿ�� �뷮 ��������")]
    Image dadGageImage;
    [SerializeField, Header("���� Ÿ�� �뷮 ��������")]
    Image brotherGageImage;
    
    
    [SerializeField]
    Text broPerText;
    [SerializeField]
    Text dadPerText;


    private void Awake()
    {
        ResetGame();
    }
    private void Update()
    {
        // ���� ���� ����
        if (GameManager.Instance.dadTowerGage >= maxTowerGage && GameManager.Instance.brotherTowerGage >= maxTowerGage)
        {
            // ���� ����
            PlayerPrefs.SetInt("SCORE", GameManager.Instance.score);
            UIManager.Instance.SceneChange("Over");
        }
        CheckGage();
    }

    private void ResetGame()
    {
        // Ÿ�� ����
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
