using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public abstract class ProgramIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Image coolImage;
    [SerializeField]
    ParticleSystem upgradeEffect;

    [SerializeField]
    float curCoolTime;
    float coolTime;
    private bool canExecute;

    public int level = 1;
    private const int MAX_LEVEL = 3;

    public Image image;
    public Button upgradeButton;
    private Image upgradeButtonImage;
    private int[] cost = { 10000, 50000 };
    public Sprite[] sprites;

    public Image cooltimeImage;
    private bool isNotice = false;
    public string programName;

    public Material upgradeMaterial;
    public Image upgradeRenderer;

    private Sequence zoomSequence;

    protected virtual void Awake()
    {

    }

    private void Start()
    {
        upgradeButtonImage = upgradeButton.GetComponent<Image>();
        upgradeButton.onClick.AddListener(LevelUp);
    }

    private void Update()
    {
        if (level > 0 && level < MAX_LEVEL)
        {
            if (!isNotice && GameManager.Instance.gold >= cost[level - 1])
            {
                upgradeButton.transform.DOKill();

                if (zoomSequence == null)
                {
                    zoomSequence = DOTween.Sequence().SetAutoKill(false);
                    zoomSequence.Append(upgradeButton.transform.DOScale(1.1f, 0.7f));
                    zoomSequence.Append(upgradeButton.transform.DOScale(1f, 0.4f));

                    zoomSequence.SetLoops(-1, LoopType.Restart);
                }
                else
                {
                    zoomSequence.Restart();
                }

                string info = $"{programName} ·¹º§ {level + 1}";
                NoticeManager.AddNotice(image.sprite, info);
                upgradeButtonImage.color = Color.white;

                if (level == MAX_LEVEL - 1)
                {
                    upgradeRenderer.material = upgradeMaterial;
                    upgradeButtonImage.material.SetColor("_BaseColor", Color.white);
                }
                isNotice = true;
            }
            else if (isNotice && GameManager.Instance.gold < cost[level - 1])
            {
                DisableUpgrade();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(coolImage.fillAmount<=0.02f)
        {
            if (!canExecute)
            {
                StartCoroutine(OnFirstClick());
            }
            else
            {
                ExecuteProgram();
            }
        }
    }

    private IEnumerator OnFirstClick()
    {
        if (canExecute) yield break;

        canExecute = true;
        yield return new WaitForSeconds(0.5f);
        canExecute = false;
    }
    public void OnCoolTime()
    {
        StartCoroutine(OnCool());
    }
    IEnumerator OnCool()
    {
        coolImage.gameObject.SetActive(true);
        coolTime = curCoolTime;
        while (coolTime > 0.0f)
        {
            coolTime -= Time.deltaTime;
            coolImage.fillAmount = (coolTime / curCoolTime);
            yield return new WaitForFixedUpdate();
        }
        coolImage.gameObject.SetActive(false);
    }

    public void LevelUp()
    {
        if (GameManager.Instance.gold >= cost[level - 1])
        {
            GameManager.Instance.gold -= cost[level - 1];

            level++;
            ++GameManager.Instance.levelArray[transform.GetSiblingIndex()];
            GameManager.Instance.AddLevelCount();

            if (level != MAX_LEVEL)
                isNotice = false;

            DisableUpgrade();

            upgradeEffect.Play();
            SoundManager.Instance.SfxSoundOn(14);
        }
        else
        {
            return;
        }

        if (image)
            image.sprite = sprites[level - 1];
        if (cooltimeImage)
            cooltimeImage.sprite = sprites[level - 1];

        if (level == MAX_LEVEL)
        {
            upgradeButton.gameObject.SetActive(false);
        }

        ChildLevelUp();
    }

    private void DisableUpgrade()
    {
        zoomSequence.Pause();
        isNotice = false;
        upgradeButtonImage.color = Color.gray;
        upgradeRenderer.material = null;

        if (level == MAX_LEVEL - 1)
        {
            upgradeButtonImage.material = null;
        }
    }

    protected virtual void ChildLevelUp()
    {

    }

    protected abstract void ExecuteProgram();

    private void OnDisable()
    {
        zoomSequence.Kill();
    }
}
