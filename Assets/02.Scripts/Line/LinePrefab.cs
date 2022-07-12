//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LinePrefab : MonoBehaviour
//{
//    private LineRenderer _line;
//    public Material defaultMaterial;

//    public void SetData(Transform parent, Vector3 pos)
//    {
//        transform.parent = parent;
//        transform.position = pos;

//        _line.startWidth = 0.003f;
//        _line.endWidth = 0.003f;
//        _line.numCornerVertices = 5;
//        _line.numCapVertices = 5;

//        int randColorI = Random.Range(0, 100);
//        if (randColorI < 20) defaultMaterial.color = Color.red;
//        else if (randColorI < 50) defaultMaterial.color = Color.blue;
//        else if (randColorI < 80) defaultMaterial.color = Color.yellow;
//        else defaultMaterial.color = Color.cyan;
//        _line.material = defaultMaterial;

//        lineRend.SetPosition(0, mousePos);
//        lineRend.SetPosition(1, mousePos);
//        mousePos.y = 0;
//    }

//    private void Update()
//    {
//        _line.material.SetColor("_Color", Color.red);
//        _line.material.SetFloat("_Inten", )
//        Color.Lerp(a, b, Time)
//    }
//}
