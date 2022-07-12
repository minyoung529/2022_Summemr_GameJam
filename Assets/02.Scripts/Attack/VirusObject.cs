using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VirusObject : MonoBehaviour
{
    private List<Monster> monsters;
    private Monster targetMonster;

    public float speed = 6f;

    private void OnEnable()
    {
        monsters = new List<Monster>(FindObjectsOfType<Monster>());
        Monster monster = monsters.OrderBy(x => Vector3.Distance(Vector3.zero, x.transform.position)).ToArray()[0];
        targetMonster = monster;
        transform.position = Camera.main.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetMonster.transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == targetMonster.transform)
        {
            gameObject.SetActive(false);
            targetMonster.IsVaccine = true;
        }
    }
}
