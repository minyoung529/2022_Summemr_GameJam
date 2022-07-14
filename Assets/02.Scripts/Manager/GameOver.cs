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
    Text rankText;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    MeshRenderer overMesh;

    string rank="";

    private void Awake()
    {
        bestScoreText.text = string.Format($"최고 점수 : {PlayerPrefs.GetInt("BESTSCORE")}");
        scoreText.text = string.Format($"점수 : {PlayerPrefs.GetInt("SCORE")}");
    }

    private void Start()
    {
        GetRank();
        canvas.SetActive(false);
        SoundManager.Instance.OverSound();
        overMesh.material.color = Color.black;
        Invoke("OnOver", 2f);
    }

    void GetRank()
    {
        // SS
        // S
        // A
        // B
        // C
        // D
        // F
        int score = PlayerPrefs.GetInt("SCORE");
        if (score < 1000)
        {
            rank += "F";
        }
        else if (score < 2000)
        {
            rank += "D";
        }
        else if (score < 3000)
        {
            rank += "C";
        }
        else if (score < 4000)
        {
            rank += "B";
        }
        else if (score < 5000)
        {
            rank += "A";
        }
        else if (score < 8000)
        {
            rank += "S";
        }
        else
        {
            rank += "SS";
        }
        rankText.text = string.Format($"{rank}");
    }

    private void OnOver()
    {
        canvas.SetActive(true);
        overMesh.material.color = Color.white;
    }
}
