using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    new public MeshRenderer renderer;

    public bool isDie;
    new private Collider collider;

    Camera _cam;

    public Texture angryTexture;
    bool isAngry = false;
    Vector3 offset;

    private bool isDragging = false;

    void Awake()
    {
        _cam = Camera.main;
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        file.transform.position = transform.position + Vector3.back * 0.85f;

        if (fileType == 1)
            fileImage.fillAmount = Mathf.Lerp(fileImage.fillAmount, GameManager.Instance.dadTowerGage / 100, Time.deltaTime * fullSpeed);
        if (fileType == 2)
            fileImage.fillAmount = Mathf.Lerp(fileImage.fillAmount, GameManager.Instance.brotherTowerGage / 100, Time.deltaTime * fullSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            Monster monster = collision.collider.GetComponent<Monster>();
            if (monster.IsVaccine) return;

            if (fileType == 1)
            {
                GameManager.Instance.dadTowerGage += monster.attackPower;

                if(!isAngry && GameManager.Instance.dadTowerGage >= 50f)
                {
                    isAngry = true;
                    renderer.material.SetTexture("_BaseMap", angryTexture);
                    renderer.transform.DOShakePosition(1f);
                }
                if (GameManager.Instance.dadTowerGage >= 100f)
                {
                    Die();
                }
            }
            if (fileType == 2)
            {
                GameManager.Instance.brotherTowerGage += monster.attackPower;

                if (!isAngry && GameManager.Instance.brotherTowerGage >= 50f)
                {
                    isAngry = true;
                    renderer.material.SetTexture("_BaseMap", angryTexture);
                    renderer.transform.DOShakePosition(1f);
                }

                if (GameManager.Instance.brotherTowerGage >= 100f)
                {
                    Die();

                }
            }

            if (!isDie)
                monster.DieMonsterByTower();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();

            if (!isDragging)
                monster.Die();
        }
    }

    private void OnMouseDrag()
    {
        isDragging = true;

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
                pos.x = transform.position.x;
            }
            if (Mathf.Abs(pos.z) > 3)
            {
                pos.z = transform.position.z;
            }

            transform.position = pos;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Die()
    {
        if (isDie) return;
        isDie = true;
        collider.isTrigger = true;
        FindObjectOfType<MonsterSpawner>().tower.Remove(this);
        renderer.material.DOColor(new Color32(144, 24, 0, 255), 3f);
    }

    private void OnMouseDown()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            offset = hitInfo.point - transform.position;
        }
    }
}
