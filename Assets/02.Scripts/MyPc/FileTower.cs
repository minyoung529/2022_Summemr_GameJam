using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileTower : MonoBehaviour
{
    // TYPE 1 == 아빠
    // TYPE 2 == 오빠
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
        file.transform.position = _cam.WorldToScreenPoint(transform.position + new Vector3(0, -0.8f, 0));

        if (fileType == 1)
            fileImage.fillAmount = Mathf.Lerp(fileImage.fillAmount, GameManager.Instance.dadTowerGage / 100, Time.deltaTime * fullSpeed);
        if (fileType == 2)
            fileImage.fillAmount = Mathf.Lerp(fileImage.fillAmount, GameManager.Instance.brotherTowerGage / 100, Time.deltaTime * fullSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            //공격 받음 type에 따라 공격받은 거점 용량 증가
            if (fileType == 1)
            {
                GameManager.Instance.dadTowerGage += 10;
            }
            if (fileType == 2)
            {
                GameManager.Instance.brotherTowerGage += 10;
            }

            collision.transform.GetComponent<Monster>().Die();
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("gdgf");

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
