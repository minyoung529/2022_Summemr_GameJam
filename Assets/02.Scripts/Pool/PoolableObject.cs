using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    /// <summary>
    /// �ش� ������Ʈ�� Ǫ���Ҷ� �ʱ�ȭ�� �������� ���Ǵ� �Լ�
    /// </summary>
    public abstract void Reset();
}
