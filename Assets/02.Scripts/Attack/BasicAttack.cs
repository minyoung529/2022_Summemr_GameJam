using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class 
    BasicAttack : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float attackDistance = 0.7f;

    public GameObject target;

    private Camera mainCam;
    private List<Monster> monster;
    private Vector3 attackPosition;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !target.activeSelf)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            GenerateEffect();
            Attack();
        }
    }

    private void Attack()
    {
        monster = GameManager.Instance.monsters;

        Predicate<Monster> match = (x) => Vector3.Distance(x.transform.position, attackPosition) < attackDistance;
        List<Monster> targetMonster = monster.FindAll(match);

        foreach(Monster monster in targetMonster)
        {
            monster.Damaged();
            //monster.ExplosionDamage(attackPosition, force);
        }
    }

    private void GenerateEffect()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        GameObject effect = null;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            attackPosition = hitInfo.point;
            effect = Instantiate(effectPrefab, attackPosition, Quaternion.Euler(Vector3.right * -90f), null);
        }

        Destroy(effect, 0.8f);
    }
}