using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text bestScoreText;
    [SerializeField]
    GameObject canvas;

    [SerializeField]
    MeshRenderer overMesh;

    private void Awake()
    {
        bestScoreText.text = string.Format($"�ְ� ���� : {PlayerPrefs.GetInt("BESTSCORE")}");
        scoreText.text = string.Format($"���� : {PlayerPrefs.GetInt("SCORE")}");
    }

    private void Start()
    {
        canvas.SetActive(false);
        SoundManager.Instance.OverSound();
        overMesh.material.color = Color.black;
        Invoke("OnOver", 2f);
    }

    private void OnOver()
    {
        canvas.SetActive(true);
        overMesh.material.color = Color.white;
    }
}
