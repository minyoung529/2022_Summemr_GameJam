using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameOver { get;  set; }
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text bestScoreText;
    [SerializeField]
    MeshRenderer startMat;
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    private MonsterSpawner spawner;

    [SerializeField]
    Text goldText;

    public int score;
    public int bestScore;
    public int gold;

    // 현재 게이지
    public float dadTowerGage;
    public float brotherTowerGage;

    public int levelCount = 0;
    public Renderer wallpaperRenderer;
    public Renderer crtRenderer;

    public Texture[] textures;

    public int[] levelArray;
    int maxLevel = 2;

    [HideInInspector]
    public List<Monster> monsters;

    public bool isOpenMenu = false;

    [SerializeField] private Vector2 fieldSize = new Vector2(9f, 5f);
    public Vector2 FieldSize => fieldSize;

    public void OnCoolTime(Image cool, float coolTime)
    {
        cool.gameObject.SetActive(true);
        cool.fillAmount = Mathf.Lerp(0, 1, coolTime);
        cool.fillAmount = Mathf.Lerp(1, 0, coolTime);
    }

    private void Start()
    {
        Time.timeScale = 1;
        GameStartReset();
        InvokeRepeating("UpScore", 1f, 1f);

        for(int i = 0; i < levelArray.Length; ++i)
        {
            levelArray[i] = 1;
        }
    }
    private void Update()
    {
        UpdateScore();
        UpdateGold();
    }

    public void SetMenu()
    {
        if (!isOpenMenu)
        {
            MenuOpen();
        }
        else
        {
            MenuClose();
        }
    }

    private void MenuOpen()
    {
        isOpenMenu = true;
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void MenuClose()
    {
        if (menuPanel)
        {
            isOpenMenu = false;
            Time.timeScale = 1;
            menuPanel.SetActive(false);
        }
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

        spawner.AddPoint(score);
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

        foreach (int level in levelArray)
        {
            if (level < maxLevel)
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            StartCoroutine(ChangeWallpaper());
            ++maxLevel;
        }
    }

    private IEnumerator ChangeWallpaper()
    {
        yield return null;

        for (int i = 4 * levelCount; i < 4 * (levelCount + 1); i++)
        {
            wallpaperRenderer.material.SetTexture("_BaseMap", textures[i]);

            crtRenderer.material.EnableKeyword("RGB_STRIPES_ON");
            crtRenderer.material.SetFloat("RGB_STRIPES_ON", 1.0f);
            yield return new WaitForSeconds(0.07f);

            crtRenderer.material.DisableKeyword("RGB_STRIPES_ON");
            crtRenderer.material.SetFloat("RGB_STRIPES_ON", 0.0f);
            yield return new WaitForSeconds(1.8f);
        }

        levelCount++;
    }

    [ContextMenu("돈")]
    public void Cheat()
    {
        gold += 100000;
    }
}
