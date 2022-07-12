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
        bestScoreText.text = string.Format($"최고 점수 : {PlayerPrefs.GetInt("BESTSCORE")}");
        scoreText.text = string.Format($"점수 : {PlayerPrefs.GetInt("SCORE")}");
    }
}
