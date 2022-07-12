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

    private void Awake()
    {
        bestScoreText.text = string.Format($"�ְ� ���� : {PlayerPrefs.GetInt("BESTSCORE")}");
        scoreText.text = string.Format($"���� : {PlayerPrefs.GetInt("SCORE")}");
    }
}
