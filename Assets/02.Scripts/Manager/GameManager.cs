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
    Text goldText;

    public int score;
    public int bestScore;
    public int gold;
    
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
        bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);
        bestScoreText.text = string.Format($"BEST SCORE\n{bestScore}");
        InvokeRepeating("UpScore", 1f, 1f);
    }
    private void Update()
    {
        UpdateScore();
        UpdateGold();
    }

    void UpdateScore()
    {
        scoreText.text = string.Format($"SCORE\n{score}");
    }

    void UpdateGold()
    {
        goldText.text = string.Format($"GOLD\n{gold}");
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
}
