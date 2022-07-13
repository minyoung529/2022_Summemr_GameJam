using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster[] monsterPrefab;
    [SerializeField] private Transform[] tower;
    public Transform[] Tower => tower;
    [SerializeField] private Transform wallPaper;
    [SerializeField] private float spawnRange = 15f;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private int poolingMonsterCount = 100;

    private float time = 0f;

    void Start()
    {
        for (int i = 0; i < (int)MonsterType.COUNT; i++)
        {
            PoolManager.Instance.CreatePool(monsterPrefab[i], poolingMonsterCount);
        }

        StartCoroutine(SpawnMonster());
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            Monster obj = PoolManager.Instance.Pop(monsterPrefab[GetMonsterType()]) as Monster;
            GameManager.Instance.monsters.Add(obj);
            obj.transform.position = GetRandomCirclePoint();
            obj.spawner = this;
            obj.SetTarget(tower[Random.Range(0, tower.Length)]);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private int GetMonsterType()
    {
        if(time <= 50f)
        {
            return (int)MonsterType.BASIC;
        }
        else
        {
            spawnDelay = 0.1f;
        }

        int rand = Random.Range(1, 101);

        if(rand >= 1 && rand <= 70)
            return (int)MonsterType.BASIC;
     
        else if(rand >= 71 && rand <= 80)
            return (int)MonsterType.FAST;
        
        else
            return (int)MonsterType.SLOW;
    }

    private Vector3 GetRandomCirclePoint()
    {
        Vector2 point = Random.insideUnitCircle.normalized * spawnRange;
        return new Vector3(point.x, wallPaper.position.y + 1, point.y);
    }
}