using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text bestScoreText;
    [SerializeField]
    MeshRenderer startMat;

    public int score;
    public int bestScore;
    
    // 현재 게이지
    public float dadTowerGage;
    public float brotherTowerGage;

    public void OnCoolTime(Image cool, float coolTime)
    {
        cool.gameObject.SetActive(true);
        cool.fillAmount = Mathf.Lerp(0, 1, coolTime);
        cool.fillAmount = Mathf.Lerp(1, 0, coolTime);
    }

    private void Awake()
    {
        GameStartReset();
        InvokeRepeating("UpScore", 1f, 1f);
    }
    private void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = string.Format($"SCORE\n{score}");
    }

    void UpScore()
    {
        score += 10;
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BESTSCORE", bestScore);
        }
    }

    void GameStartReset()
    {
        score = 0;
        startMat.material.color = Color.white;
        bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);
        bestScoreText.text = string.Format($"BEST SCORE\n{bestScore}");
    }

}
