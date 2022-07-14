using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster[] monsterPrefab;
    public List<FileTower> tower;
    [SerializeField] private Transform wallPaper;
    [SerializeField] private Vector2 spawnRange = new Vector2(15f, 10f);
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
            obj.SetTarget(tower[Random.Range(0, tower.Count)].transform);
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
        Vector2 point = Random.Range(0, 2) == 1 ? new Vector2(spawnRange.x, Random.Range(-spawnRange.y, spawnRange.y)) : new Vector2(Random.Range(-spawnRange.x, spawnRange.x), spawnRange.y);
        point *= Random.Range(0, 2) == 1 ? 1 : -1;
        return new Vector3(point.x, wallPaper.position.y + 1, point.y);
    }
}