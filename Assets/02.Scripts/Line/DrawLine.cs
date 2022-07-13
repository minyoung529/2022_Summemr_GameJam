using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    Transform attackPos;

    public PoolableObject bullet;

    [SerializeField]
    GameObject[] msPaint;

    [SerializeField]
    GameObject gageCanvas;


    GameObject madeLine;
    public Material defaultMaterial;
    private LineRenderer curLine;
    private int positionCount = 2;
    private Vector3 PrevPos = Vector3.zero;

    private Camera _mainCam;

    public LayerMask _whatIsBoard;

    bool isCreate = false;
    bool isCreateNow = false;

    [HideInInspector]
    public int bulletCount = 10;

    [HideInInspector]
    public int level = 1;

    private void Awake()
    {
        _mainCam = Camera.main;
        bulletCount = 10;
    }

    void Update()
    {
        DrawMouse();
    }

    // 마우스 드래그로 그리기
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

    // 라인 만들기
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
        if (randColorI < 20) defaultMaterial.SetColor("_Color", Color.red);
        else if (randColorI < 50) defaultMaterial.SetColor("_Color", Color.blue);
        else if (randColorI < 80) defaultMaterial.SetColor("_Color", Color.yellow);
        else defaultMaterial.SetColor("_Color", Color.cyan);
        lineRend.material = defaultMaterial;

        lineRend.SetPosition(0, mousePos);
        lineRend.SetPosition(1, mousePos);
        mousePos.y = 0;

        curLine = lineRend;
    }

    // 라인 연결
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

    // 공격 준비
    void ActionReady()
    {
        madeLine.transform.SetParent(null);
        msPaint[0].SetActive(false);
        msPaint[1].SetActive(false);

        SoundManager.Instance.SfxSoundOn(0);

        // 오브젝트 사이즈 줄어들고 왼쪽 아래로 이동
        LineRenderer lr = madeLine.GetComponent<LineRenderer>();
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 v = lr.GetPosition(i);
            v += new Vector3(-0.18f, 0, -0.08f);
            lr.SetPosition(i, v);
        }

        for (int i = 0; i < bulletCount; i++)
        {
            PaintingBullet obj = PoolManager.Instance.Pop(bullet) as PaintingBullet;
            obj.transform.position = attackPos.position;

            Vector3 euler = obj.transform.eulerAngles;
            euler.y = Random.Range(0, 360f);
            obj.transform.eulerAngles = euler;
            obj.ChangeColor(level);

            float x = Random.Range(0f, 1f);
            float z = Random.Range(0f, 1f);
            obj.SetDirection(new Vector3(x, 0, z));

            obj.transform.localScale = Vector3.one * Random.Range(transform.localScale.x - 0.1f, transform.localScale.x + 0.1f);
        }

        defaultMaterial.color *= 300f;
        defaultMaterial.color += Color.white / 255f;
        Destroy(madeLine, 2f);
        isCreate = false;
    }

    private void OnEnable()
    {
        gageCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        gageCanvas.SetActive(true);
    }
}
