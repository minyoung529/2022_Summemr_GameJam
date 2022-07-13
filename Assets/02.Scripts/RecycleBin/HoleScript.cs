using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public PolygonCollider2D hole2DCollider;
    public PolygonCollider2D ground2DCollider;
    public MeshCollider GeneratedMeshCollider;
    public float initialScale;
    private BoxCollider range;
    Mesh GeneratedMesh;

    public float holeTime;
    private float holeSize = 1f;
    public float HoleSize
    {
        get => holeSize;
        set
        {
            holeSize = value;
        }
    }

    public void Awake()
    {
        range = GetComponent<BoxCollider>();
    }

    public void EnableHole()
    {
        StartCoroutine(ExtendHole());
    }

    IEnumerator ExtendHole()
    {
        range.enabled = true;
        float time = 0;
        while(time <= 1f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * holeSize, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void DisableHole()
    {
        StartCoroutine(ContractHole());
    }

    IEnumerator ContractHole()
    {
        float time = 0;
        while (time <= 1f)
        {
            transform.localScale = Vector3.Lerp(Vector3.one * holeSize, Vector3.zero, time);
            time += Time.deltaTime;
            yield return null;
        }
        range.enabled = false;
    }

    private void FixedUpdate()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = transform.localScale * holeSize;
            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCollider.GetPath(0);
        for(int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DCollider.transform.TransformPoint(PointPositions[i]);
        }
        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }

    private void Make3DMeshCollider()
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2DCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GeneratedMesh;
    }
}
