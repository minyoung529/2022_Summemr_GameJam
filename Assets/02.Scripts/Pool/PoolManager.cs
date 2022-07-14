using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<string, Pool<PoolableObject>> pools = new Dictionary<string, Pool<PoolableObject>>();

    /// <summary>
    /// prefab을 count만큼 복제하여 PoolManager가 담아둠
    /// </summary>
    /// <param name="prefab">풀링할 PoolableObject 스크립트를 상속받는 프래팹</param>
    /// <param name="count">풀링할 수</param>
    public void CreatePool(PoolableObject prefab, int count = 20)
    {
        Pool<PoolableObject> pool = new Pool<PoolableObject>(prefab, transform, count);
        pools.Add(prefab.name, pool);
    }

    /// <summary>
    /// prefab 매개변수의 이름을 이용하여 오브젝트를 찾아와 Active를 true로 변경 하고 prefab의 Reset 함수 실행 후  리턴함
    /// </summary>
    /// <param name="prefab"></param>
    public PoolableObject Pop(PoolableObject prefab)
    {
        if (!pools.ContainsKey(prefab.name))
        {
            CreatePool(prefab);
        }
        PoolableObject item = pools[prefab.name].Pop();
        return item;
    }

    /// <summary>
    /// obj를 풀매니저에 Active를 false로 변경하고 푸쉬
    /// </summary>
    /// <param name="obj">Push할 대상</param>
    public void Push(PoolableObject obj)
    {
        if (pools.ContainsKey(obj.name))
        {
            pools[obj.name].Push(obj);
        }
    }
}
