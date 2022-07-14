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
        // StartCoroutine(SpawnMonster());
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            if (tower.Count == 0) yield break;
            Monster obj = PoolManager.Instance.Pop(monsterPrefab[GetMonsterType()]) as Monster;
            GameManager.Instance.monsters.Add(obj);
            obj.transform.position = GetRandomCirclePoint();
            obj.spawner = this;
            obj.SetTarget(tower[Random.Range(0, tower.Count)].transform);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnVerArr(MonsterType type, int cnt, float dis)
    {
        Monster centerMonster = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
        GameManager.Instance.monsters.Add(centerMonster);
        centerMonster.transform.position = GetRandomCirclePoint();
        centerMonster.spawner = this;
        int towerT = Random.Range(0, tower.Count);
        centerMonster.SetTarget(tower[towerT].transform);

        for (int i=1;i<=cnt;i++)
        {
            Monster obj = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
            Monster obj2 = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
            GameManager.Instance.monsters.Add(obj);
            GameManager.Instance.monsters.Add(obj2);
            obj.transform.position = centerMonster.transform.position+new Vector3(0, 0, dis*i);
            obj2.transform.position = centerMonster.transform.position+new Vector3(0, 0, (-dis)*i);
            obj.spawner = this;
            obj2.spawner = this;
            obj.SetTargetRigid(centerMonster.rigid);
            obj2.SetTargetRigid(centerMonster.rigid);
        }
    }
    void SpawnHorArr(MonsterType type, int cnt, float dis)
    {
        Monster centerMonster = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
        GameManager.Instance.monsters.Add(centerMonster);
        centerMonster.transform.position = GetRandomCirclePoint();
        centerMonster.spawner = this;
        int towerT = Random.Range(0, tower.Count);
        centerMonster.SetTarget(tower[towerT].transform);

        for (int i = 1; i <= cnt; i++)
        {
            Monster obj = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
            Monster obj2 = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
            GameManager.Instance.monsters.Add(obj);
            GameManager.Instance.monsters.Add(obj2);
            obj.transform.position = centerMonster.transform.position + new Vector3(dis * i, 0, 0);
            obj2.transform.position = centerMonster.transform.position + new Vector3((-dis) * i, 0, 0);
            obj.spawner = this;
            obj2.spawner = this;
            obj.SetTargetRigid(centerMonster.rigid);
            obj2.SetTargetRigid(centerMonster.rigid);
        }
    }

    private int GetMonsterType()
    {
        if (time <= 50f)
        {
            return (int)MonsterType.BASIC;
        }
        else
        {
            spawnDelay = 0.1f;
        }

        int rand = Random.Range(1, 101);

        if (rand >= 1 && rand <= 70)
            return (int)MonsterType.BASIC;

        else if (rand >= 71 && rand <= 80)
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

    private void CirclePattern(float radius, MonsterType type, int count)
    {
        int target = Random.Range(0, tower.Count);
        Vector3 center = GetRandomCirclePoint();

        if (center.x > 0)
            center.x += radius;
        else
            center.x -= radius;

        if(center.z > 0)
            center.z += radius;
        else
            center.z -= radius;

        Monster centerMonster = null;

        for (int i = 0; i < count; i++)
        {
            Monster obj = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
            GameManager.Instance.monsters.Add(obj);

            Vector3 position = center;

            position.x += Random.Range(-radius, radius);
            position.z += Random.Range(-radius, radius);
            obj.transform.position = position;

            obj.spawner = this;

            if (i == 0)
            {
                obj.SetTarget(tower[target].transform);
                centerMonster = obj;
            }
            else
            {
                if (centerMonster)
                    obj.SetTargetRigid(centerMonster.rigid);
            }
        }
    }
}