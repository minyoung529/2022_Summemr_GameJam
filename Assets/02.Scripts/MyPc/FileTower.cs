using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileTower : MonoBehaviour
{
    // TYPE 1 == 아빠
    // TYPE 2 == 오빠
    [SerializeField]
    int fileType;

    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log($"BROTHER : {GameManager.Instance.brotherTowerGage}");
            Debug.Log($"DAD : {GameManager.Instance.dadTowerGage}");
        if(collision.collider.CompareTag("Monster"))
        {
            //공격 받음 type에 따라 공격받은 거점 용량 증가
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
