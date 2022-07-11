using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    /// <summary>
    /// 해당 오브젝트를 푸쉬할때 초기화할 목적으로 사용되는 함수
    /// </summary>
    public abstract void Reset();
}
