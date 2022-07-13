using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFile : MonoBehaviour
{
    private Material fileMaterial;
    public Color selectColor;

    private void Start()
    {
        fileMaterial = GetComponent<Renderer>().material;
    }

    private void OnMouseEnter()
    {
        Color32 color = selectColor;
        color.a -= 40;
        fileMaterial.color = color;
    }

    private void OnMouseExit()
    {
        fileMaterial.color = Color.clear;
    }

    private void OnMouseDown()
    { 
        fileMaterial.color = selectColor;
    }
}