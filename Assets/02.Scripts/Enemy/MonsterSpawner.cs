using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Transform tower;
    public Transform Tower => tower;
    [SerializeField] private Transform wallPaper;
    [SerializeField] private float spawnRange = 15f;
    [SerializeField] private float spawnDelay = 1f;
    
    void Start()
    {
        PoolManager.Instance.CreatePool(monsterPrefab, 50);
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            Monster obj = PoolManager.Instance.Pop(monsterPrefab) as Monster;
            obj.transform.position = GetRandomCirclePoint();
            obj.spawner = this;
            obj.SetTarget(tower);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector3 GetRandomCirclePoint()
    {
        Vector2 point = Random.insideUnitCircle.normalized * spawnRange;
        return new Vector3(point.x, wallPaper.position.y + 1, point.y);
    }
}
