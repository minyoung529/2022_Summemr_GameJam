using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster[] monsterPrefab;
    public List<FileTower> tower;
    [SerializeField] private Transform wallPaper;
    [SerializeField] private Vector2 spawnRange = new Vector2(15f, 10f);
    [SerializeField] private float delayFactor = 1f;
    [SerializeField] private float spawnDelay = 1f;
    private float currentDelay = 1f;
    [SerializeField] private float squareDelay = 15f;
    [SerializeField] private int poolingMonsterCount = 100;

    private WaitForSeconds waitSquareDelay;

    private float time = 0f;

    private bool isSquareCoroutine = false;
    private bool isHorVer = false;

    void Start()
    {
        currentDelay = spawnDelay;
        for (int i = 0; i < (int)MonsterType.COUNT; i++)
        {
            PoolManager.Instance.CreatePool(monsterPrefab[i], poolingMonsterCount);
        }
        // StartCoroutine(SpawnMonster());

        waitSquareDelay = new WaitForSeconds(squareDelay);

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
        centerMonster.transform.position = GetLeftRightPoint();
        centerMonster.spawner = this;

        int towerT = Random.Range(0, tower.Count);
        centerMonster.SetTarget(tower[towerT].transform);

        for (int i = 1; i <= cnt; i++)
        {
            Monster obj = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
            Monster obj2 = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
            GameManager.Instance.monsters.Add(obj);
            GameManager.Instance.monsters.Add(obj2);
            obj.transform.position = centerMonster.transform.position + new Vector3(0, 0, dis * i);
            obj2.transform.position = centerMonster.transform.position + new Vector3(0, 0, (-dis) * i);
            obj.spawner = this;
            obj2.spawner = this;
            obj.SetTargetRigid(centerMonster);
            obj2.SetTargetRigid(centerMonster);
        }
    }
    void SpawnHorArr(MonsterType type, int cnt, float dis)
    {
        Monster centerMonster = PoolManager.Instance.Pop(monsterPrefab[(int)type]) as Monster;
        GameManager.Instance.monsters.Add(centerMonster);
        centerMonster.transform.position = GetTopBottomPoint();
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
            obj.SetTargetRigid(centerMonster);
            obj2.SetTargetRigid(centerMonster);
        }
    }

    private int GetMonsterType()
    {
        if (time <= 70f)
        {
            return (int)MonsterType.BASIC;
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

    private Vector3 GetTopBottomPoint()
    {
        Vector2 point = new Vector2(Random.Range(-spawnRange.x, spawnRange.x), spawnRange.y);
        point.y *= Random.Range(0, 2) == 1 ? 1 : -1;
        return new Vector3(point.x, wallPaper.position.y + 1, point.y);
    }

    private Vector3 GetLeftRightPoint()
    {
        Vector2 point = new Vector2(spawnRange.x, Random.Range(-spawnRange.y, spawnRange.y));
        point.x *= Random.Range(0, 2) == 1 ? 1 : -1;
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

        if (center.z > 0)
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
                    obj.SetTargetRigid(centerMonster);
            }
        }
    }

    internal void AddPoint(int score)
    {
        if (score > 3100 && !isSquareCoroutine)
        {
            isSquareCoroutine = true;
            StartCoroutine(SquareCoroutine());
        }
        if (score > 2000 && !isHorVer)
        {
            isHorVer = true;
            StartCoroutine(VerHorCoroutine());
        }

        currentDelay = spawnDelay / (time * delayFactor);
    }

    public void Wave()
    {

    }

    private IEnumerator SquareCoroutine()
    {
        while (true)
        {
            if (tower.Count == 0) yield break;

            CirclePattern(Random.Range(3f, 5f), MonsterType.FAST, 60);
            Debug.Log("Ciircle");
            yield return new WaitForSeconds(10f);
        }
    }

    private IEnumerator VerHorCoroutine()
    {
        while (true)
        {
            if (tower.Count == 0) yield break;

            if (Random.Range(0, 2) == 0)
            {
                SpawnHorArr((MonsterType)Random.Range(1, 3), 3, 2);
            }
            else
            {
                SpawnVerArr((MonsterType)Random.Range(1, 3), 3, 2);
            }

            yield return new WaitForSeconds(10f);
        }
    }
}