using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVaccineCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            monster?.VaccineCollisionEnter();
        }
    }
}
