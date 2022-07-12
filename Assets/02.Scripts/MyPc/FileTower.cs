using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileTower : MonoBehaviour
{
    // TYPE 1 == �ƺ�
    // TYPE 2 == ����
    [SerializeField]
    int fileType;

    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log($"BROTHER : {GameManager.Instance.brotherTowerGage}");
            Debug.Log($"DAD : {GameManager.Instance.dadTowerGage}");
        if(collision.collider.CompareTag("Monster"))
        {
            //���� ���� type�� ���� ���ݹ��� ���� �뷮 ����
            if(fileType==1)
            {
                GameManager.Instance.dadTowerGage += 10;
            }
            if(fileType==2)
            {
                GameManager.Instance.brotherTowerGage += 10;
            }
        }
    }
}
