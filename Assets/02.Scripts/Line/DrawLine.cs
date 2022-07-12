using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    Transform attackPos;
    [SerializeField]
    GameObject[] bullet;
    [SerializeField]
    GameObject[] msPaint;

    GameObject madeLine;
    public Material defaultMaterial;
    private LineRenderer curLine;
    private int positionCount = 2;
    private Vector3 PrevPos = Vector3.zero;

    private Camera _mainCam;

    public LayerMask _whatIsBoard;

    bool isCreate = false;
    bool isCreateNow = false;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    void Update()
    {
        DrawMouse();
    }


    // ���콺 �巡�׷� �׸���
    void DrawMouse()
    {
        Vector3 mousePos = _mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3f));
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        bool isIn = false;
        Physics.Raycast(ray, out hit, 100f, _whatIsBoard);

        isIn = hit.collider != null && hit.collider.CompareTag("MSPAINT");
        if (isCreateNow)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isCreateNow = false;
                ActionReady();
            }
        }

        if (!isIn) return;

        if (Input.GetMouseButtonDown(0))
        {
            isCreate = true;
            isCreateNow = true;
            createLine(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
            if (!isCreate) return;
            isCreateNow = true;
            connectLine(mousePos);
        }
    }

    // ���� �����
    void createLine(Vector3 mousePos)
    {
        positionCount = 2;
        GameObject line = new GameObject("Line");
        LineRenderer lineRend = line.AddComponent<LineRenderer>();
        line.AddComponent<MeshCollider>();

        madeLine = line;

        line.transform.parent = transform;
        line.transform.position = mousePos;

        lineRend.startWidth = 0.003f;
        lineRend.endWidth = 0.003f;
        lineRend.numCornerVertices = 5;
        lineRend.numCapVertices = 5;

        int randColorI = Random.Range(0, 100);
        if (randColorI < 20) defaultMaterial.color = Color.red;
        else if (randColorI < 50) defaultMaterial.color = Color.blue;
        else if (randColorI < 80) defaultMaterial.color = Color.yellow;
        else defaultMaterial.color = Color.cyan;
        lineRend.material = defaultMaterial;

        lineRend.SetPosition(0, mousePos);
        lineRend.SetPosition(1, mousePos);
        mousePos.y = 0;

        curLine = lineRend;
    }

    // ���� ����
    void connectLine(Vector3 mousePos)
    {
        if (PrevPos != null && Mathf.Abs(Vector3.Distance(PrevPos, mousePos)) >= 0.001f)
        {
            PrevPos = mousePos;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mousePos);
        }
    }

    // ���� �غ�
    void ActionReady()
    {
        madeLine.transform.SetParent(null);
        msPaint[0].SetActive(false);
        msPaint[1].SetActive(false);

        // ������Ʈ ������ �پ��� ���� �Ʒ��� �̵�
        LineRenderer lr = madeLine.GetComponent<LineRenderer>();
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 v = lr.GetPosition(i);
            v += new Vector3(-0.18f, 0, -0.08f);
            lr.SetPosition(i, v);
        }

        // 3���� �Ѿ��� Ű��
        bullet[0].transform.position = attackPos.position;
        bullet[1].transform.position = attackPos.position;
        bullet[2].transform.position = attackPos.position;
        bullet[0].SetActive(true);
        bullet[1].SetActive(true);
        bullet[2].SetActive(true);

        Destroy(madeLine, 2f);
        isCreate = false;
    }

}
