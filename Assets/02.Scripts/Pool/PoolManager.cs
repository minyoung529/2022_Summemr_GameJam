using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<string, Pool<PoolableObject>> pools = new Dictionary<string, Pool<PoolableObject>>();

    /// <summary>
    /// prefab�� count��ŭ �����Ͽ� PoolManager�� ��Ƶ�
    /// </summary>
    /// <param name="prefab">Ǯ���� PoolableObject ��ũ��Ʈ�� ��ӹ޴� ������</param>
    /// <param name="count">Ǯ���� ��</param>
    public void CreatePool(PoolableObject prefab, int count = 20)
    {
        Pool<PoolableObject> pool = new Pool<PoolableObject>(prefab, transform, count);
        pools.Add(prefab.name, pool);
    }

    /// <summary>
    /// prefab �Ű������� �̸��� �̿��Ͽ� ������Ʈ�� ã�ƿ� Active�� true�� ���� �ϰ� prefab�� Reset �Լ� ���� ��  ������
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
    /// obj�� Ǯ�Ŵ����� Active�� false�� �����ϰ� Ǫ��
    /// </summary>
    /// <param name="obj">Push�� ���</param>
    public void Push(PoolableObject obj)
    {
        if (pools.ContainsKey(obj.name))
        {
            pools[obj.name].Push(obj);
        }
    }
}
