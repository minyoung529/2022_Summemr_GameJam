using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBullet : MonoBehaviour
{
    [SerializeField]
    int bulletType;

    [SerializeField]
    float moveSpeed;

    Rigidbody rb;
    Vector3 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rb.AddForce(dir*Time.deltaTime*moveSpeed, ForceMode.Impulse);
        if(transform.position.z > 6 || transform.position.x > 10)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (bulletType == 1)
        {
            dir = Vector3.forward;
        }
        if (bulletType == 2)
        {
            dir = new Vector3(1, 0, 1);
        }
        if (bulletType == 3)
        {
            dir = Vector3.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag != "DRAWBULLET")
        gameObject.SetActive(false);
    }
}
