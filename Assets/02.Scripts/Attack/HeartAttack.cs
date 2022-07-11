using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAttack : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 box = new Vector3(1f, 0.01f, 1f);
    private LayerMask bottomLayer;

    [SerializeField] private float gravity;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bottomLayer = LayerMask.GetMask("Platform");

        AppearHeart();
    }

    void Update()
    {
        Gravity();
    }

    public void AppearHeart()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Random.Range(-1f, 1f);
        direction.z = Random.Range(0f, 1f);
        rigid.AddForce(direction * 5000f);
    }

    private void Gravity()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, box, transform.rotation, bottomLayer);

        if (cols.Length <= 1)
        {
            rigid.velocity =  Vector3.back * gravity;
        }
        else
        {
            Debug.Log(cols[0].name);
            rigid.velocity = Vector3.zero;
        }
    }
}
