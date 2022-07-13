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

    [SerializeField]
    Text goldText;

    public int score;
    public int bestScore;
    public int gold;

    // 현재 게이지
    public float dadTowerGage;
    public float brotherTowerGage;

    public int levelCount = 0;
    new public Renderer renderer;

    public Texture[] textures;

    public int[] levelArray = { 0, 0, 0, 0, 0 };

    [HideInInspector]
    public List<Monster> monsters;

    public void OnCoolTime(Image cool, float coolTime)
    {
        cool.gameObject.SetActive(true);
        cool.fillAmount = Mathf.Lerp(0, 1, coolTime);
        cool.fillAmount = Mathf.Lerp(1, 0, coolTime);
    }

    private void Start()
    {
        GameStartReset();
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

    void GameStartReset()
    {
        score = 0;
        startMat.material.color = Color.white;
        bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);
        bestScoreText.text = string.Format($"BEST SCORE\n{bestScore}");
    }

    public void AddLevelCount()
    {
        bool isCorrect = true;
        int num = levelArray[0];

        foreach (int level in levelArray)
        {
            if (num != level)
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            StartCoroutine(ChangeWallpaper());
            levelCount++;
        }
    }

    private IEnumerator ChangeWallpaper()
    {
        for (int i = levelCount; i < 4 * (levelCount + 1); i++)
        {
            renderer.material.SetTexture("_BaseMap", textures[i]);
            yield return new WaitForSeconds(2f);
        }
    }
}
