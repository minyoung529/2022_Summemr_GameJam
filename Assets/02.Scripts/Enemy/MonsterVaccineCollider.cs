using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVaccineCollider : MonoBehaviour
{
    Monster monster;

    private void Start()
    {
        monster = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.CompareTag("Monster") && monster.IsVaccine)
        {
            Monster monster = other.GetComponent<Monster>();
            monster?.VaccineCollisionEnter();
        }
    }
}
