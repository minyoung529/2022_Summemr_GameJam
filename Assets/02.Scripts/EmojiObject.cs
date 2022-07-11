using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiObject : MonoBehaviour
{
    [SerializeField] private GameObject[] emojis;

    private void Awake()
    {
        GameObject prefab = emojis[Random.Range(0, emojis.Length)];
        Instantiate(prefab, transform.position, prefab.transform.rotation, transform);
    }
}