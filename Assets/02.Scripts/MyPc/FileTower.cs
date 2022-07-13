using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileTower : MonoBehaviour
{
    // TYPE 1 == �ƺ�
    // TYPE 2 == ����
    [SerializeField]
    int fileType;
    [SerializeField]
    float fullSpeed;
    [SerializeField]
    GameObject file;
    [SerializeField]
    Image fileImage;

    Camera _cam;

    Vector3 offset;

    void Awake()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        file.transform.position = transform.position + Vector3.back;

        if (fileType == 1)
            fileImage.fillAmount = Mathf.Lerp(fileImage.fillAmount, GameManager.Instance.dadTowerGage / 100, Time.deltaTime * fullSpeed);
        if (fileType == 2)
            fileImage.fillAmount = Mathf.Lerp(fileImage.fillAmount, GameManager.Instance.brotherTowerGage / 100, Time.deltaTime * fullSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            //���� ���� type�� ���� ���ݹ��� ���� �뷮 ����
                int attack = collision.collider.GetComponent<Monster>().attackPower;
            if (fileType == 1)
            {
                GameManager.Instance.dadTowerGage += attack;
            }
            if (fileType == 2)
            {
                GameManager.Instance.brotherTowerGage += attack;
            }

            collision.transform.GetComponent<Monster>().DieMonster();
        }
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 pos = hitInfo.point;
            pos.y = 0f;

            pos.x -= offset.x;
            pos.z -= offset.z;

            if (Mathf.Abs(pos.x) > 5.5f)
            {
                pos.x = transform.position.x ;
            }
            if (Mathf.Abs(pos.z) > 3)
            {
                pos.z = transform.position.z;
            }

            transform.position = pos;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("DOWN");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            offset = hitInfo.point - transform.position;
        }
    }
}
