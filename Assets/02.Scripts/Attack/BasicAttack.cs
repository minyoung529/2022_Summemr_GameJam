using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private LayerMask layer;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateEffect();
        }
    }

    private void GenerateEffect()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        GameObject effect = null;

        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red, 1f);

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            effect = Instantiate(effectPrefab, hitInfo.point, Quaternion.Euler(Vector3.right * -90f), null);
        }

        Destroy(effect, 2f);
    }
}