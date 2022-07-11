using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    Transform attackPos;
    [SerializeField]
    GameObject[] bullet;

    GameObject line;
    public Material defaultMaterial;
    private LineRenderer curLine; 
    private int positionCount = 2; 
    private Vector3 PrevPos = Vector3.zero;

    bool isDrawNow = false;

    void Update()
    {
        // �� ȹ ����
        if (isDrawNow) return;

        DrawMouse();
    }

    // ���콺 �巡�׷� �׸���
    void DrawMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3f));

        if (Input.GetMouseButtonUp(0))
        {
            isDrawNow = true;
            ActionReady();
        }
        if (Input.GetMouseButtonDown(0)) createLine(mousePos);
        else if (Input.GetMouseButton(0)) connectLine(mousePos);
    }

    // ���� �����
    void createLine(Vector3 mousePos)
    {
        positionCount = 2;
        line = new GameObject("Line");
        LineRenderer lineRend = line.AddComponent<LineRenderer>();
        line.AddComponent<MeshCollider>();

        line.transform.parent = transform;
        line.transform.position = mousePos;

        lineRend.startWidth = 0.01f;
        lineRend.endWidth = 0.01f;
        lineRend.numCornerVertices = 5;
        lineRend.numCapVertices = 5;
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
        // ������Ʈ ������ �پ��� ���� �Ʒ��� �̵�
        LineRenderer lr = line.GetComponent<LineRenderer>();
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 v = lr.GetPosition(i);
            v += new Vector3(-0.18f, 0, -0.1f);
            lr.SetPosition(i, v);
        }

        // 3���� �Ѿ��� Ű��
        bullet[0].transform.position = attackPos.position;
        bullet[1].transform.position = attackPos.position;
        bullet[2].transform.position = attackPos.position;
        bullet[0].SetActive(true);
        bullet[1].SetActive(true);
        bullet[2].SetActive(true);
    }


}
