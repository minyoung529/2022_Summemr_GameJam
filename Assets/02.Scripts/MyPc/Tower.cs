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
    
    bool isBroken = false;

    public GameObject[] closeOnGameOver;

    private void Update()
    {
        if (GameManager.Instance.dadTowerGage >= maxTowerGage && GameManager.Instance.brotherTowerGage >= maxTowerGage)
        {
            if (GameManager.Instance.IsGameOver) return;

            PlayerPrefs.SetInt("SCORE", GameManager.Instance.score);

            Vector3 position = FileTower.DiePosition;
            position.y += 2f;
            GameManager.Instance.IsGameOver = true;

            foreach (var obj in closeOnGameOver)
                obj.SetActive(false);

            Sequence seq = DOTween.Sequence();
            seq.Append(Camera.main.transform.DOMove(position, 4f));
            seq.AppendInterval(2f);
            seq.AppendCallback(() => UIManager.Instance.SceneChange("Over"));
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
    }
}
