using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    private float maxDrawAmount = 2f; //그리기 제한
    private float currentDrawAmount = 2f;

    [SerializeField]
    private Transform gague;

    public PoolableObject bullet;

    [SerializeField]
    GameObject[] msPaint;

    GameObject madeLine;
    public Material defaultMaterial;
    private LineRenderer curLine;
    private int positionCount = 2;
    private Vector3 PrevPos = Vector3.zero;

    private Camera _mainCam;

    public LayerMask _whatIsBoard;

    public PaintIcon _iconP;

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

    private void OnEnable()
    {
        currentDrawAmount = maxDrawAmount;
        UpdateGague();
    }

    void Update()
    {
        if (GameManager.Instance.isOpenMenu) return;
        DrawMouse();
    }

    // 마우스 드래그로 그리기
    void DrawMouse()
    {
        Vector3 mousePos = _mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCam.transform.position.y - transform.position.y));
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

        lineRend.startWidth = 0.3f;
        lineRend.endWidth = 0.3f;
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
        if (currentDrawAmount <= 0) return;
        if (PrevPos != null && Mathf.Abs(Vector3.Distance(PrevPos, mousePos)) >= 0.001f)
        {
            PrevPos = mousePos;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mousePos + new Vector3(0, 0.1f)); //y축 방향으로 조금 움직여서 그림판 보다 앞에 위치하기 위해 더해줌
            currentDrawAmount -= (curLine.GetPosition(positionCount - 1) - curLine.GetPosition(positionCount - 2)).magnitude;
            UpdateGague();
        }
    }

    public void UpgradeMaxGague(float value)
    {
        maxDrawAmount *= value;
    }

    public void UpdateGague()
    {
        Debug.Log("게이지 업데이트");
        gague.localScale = new Vector3(currentDrawAmount / maxDrawAmount * (maxDrawAmount * 0.1f), 1, 1);
    }

    // 공격 준비
    void ActionReady()
    {
        _iconP.OnCoolTime();

        madeLine.transform.SetParent(null);
        msPaint[0].SetActive(false);
        msPaint[1].SetActive(false);

        UIManager.Instance.EnactiveWindow(msPaint[1].gameObject);

        SoundManager.Instance.SfxSoundOn(0);

        for (int i = 0; i < positionCount - 1; i++)
        {
            Vector3 dir = curLine.GetPosition(i + 1) - curLine.GetPosition(i);
            dir = Vector3.Cross(dir, Vector3.up).normalized;
            if (dir.sqrMagnitude <= 0) continue;

            for (int j = 0; j < 2; j++)
            {
                PaintingBullet obj = PoolManager.Instance.Pop(bullet) as PaintingBullet;
                Vector3 pos = curLine.GetPosition(i);
                pos.y = transform.position.y;
                obj.transform.position = pos;

                Vector3 euler = obj.transform.eulerAngles;
                euler.y = Random.Range(0, 360f);
                obj.transform.eulerAngles = euler;
                obj.ChangeColor(level);

                obj.SetDirection(dir, 3f);
                obj.transform.localScale = Vector3.one * Random.Range(transform.localScale.x - 0.1f, transform.localScale.x + 0.1f);
                dir = -dir;
            }
        }

        defaultMaterial.color *= 300f;
        defaultMaterial.color += Color.white / 255f;
        Destroy(madeLine, 2f);
        isCreate = false;
    }
}
