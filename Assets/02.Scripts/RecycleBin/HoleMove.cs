using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMove : MonoBehaviour
{
    private Camera mainCam;
    private LayerMask groundLayer;
    private void Awake()
    {
        mainCam = Camera.main;
        groundLayer = LayerMask.NameToLayer("RaycastCollider");
    }
    private void FixedUpdate()
    {
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1 << groundLayer))
        {
            Vector3 dir = hit.point;
            dir.y = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, dir, Time.deltaTime * 10f);
             
        }
        else
        {
            //Debug.Log("충돌 안함");
        }
    }
}
