using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    [SerializeField, Header("MAX Ÿ�� �뷮")]
    float maxTowerGage;
    [SerializeField, Header("�뷮 ���� �ӵ�")]
    float fullSpeed;

    [SerializeField]
    MeshRenderer overMesh;
    [SerializeField]
    GameObject brokenImage;

    // ��������
    [SerializeField, Header("�ƺ� Ÿ�� �뷮 ��������")]
    Image dadGageImage;
    [SerializeField, Header("���� Ÿ�� �뷮 ��������")]
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
            // ���
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
