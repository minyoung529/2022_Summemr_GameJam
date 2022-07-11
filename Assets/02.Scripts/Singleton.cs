using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static private T instance;
    static public T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>().GetComponent<T>();
                if(instance == null)
                {
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        T[] obj = FindObjectsOfType<T>();
        if (obj.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
