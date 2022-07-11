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
    GameObject msPaint;

    GameObject madeLine;
    public Material defaultMaterial;
    private LineRenderer curLine; 
    private int positionCount = 2; 
    private Vector3 PrevPos = Vector3.zero;

    bool isDrawNow = false;
    bool isCreate = false;
    bool isPer = false;

    void Update()
    {
        // �׸��� ���� ����
        // �� ȹ ����
        if(isPer && !isDrawNow)  DrawMouse();
        Debug.Log(isPer);
    }

    // ���콺 �巡�׷� �׸���
    void DrawMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3f));

        if (Input.GetMouseButtonUp(0))
        {
            if (!isCreate || isDrawNow) return;
            isDrawNow = true;
            isCreate = false;
            ActionReady();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isCreate = true;
            createLine(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
            if (!isCreate) return;
            connectLine(mousePos);
        }
    }

    // ���� �����
    void createLine(Vector3 mousePos)
    {
        Debug.Log("���� �׸���");
        positionCount = 2;
        GameObject line = new GameObject("Line");
        LineRenderer lineRend = line.AddComponent<LineRenderer>();
        line.AddComponent<MeshCollider>();

        madeLine = line;

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
        Debug.Log("���� ����");
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
        Debug.Log("�׸��� ��");
        madeLine.transform.SetParent(null);
        msPaint.SetActive(false);

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
    }

    private void OnMouseExit()
    {
        isPer = false;
    }
    private void OnMouseEnter()
    {
        isPer = true;
    }
}
