using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    protected int level = 1;
    private const int MAX_LEVEL = 3;

    public Image image;
    public Button upgradeButton;
    private int[] cost = { 10000, 50000 };
    public Sprite[] sprites;

    public Image cooltimeImage;

    private bool isNotice = false;

    public string programName;

    private void Start()
    {
        //image = GetComponent<Image>();
        upgradeButton.onClick.AddListener(LevelUp);
    }

    private void Update()
    {
        if (!isNotice && level > 0 && level < MAX_LEVEL && GameManager.Instance.gold >= cost[level - 1])
        {
            string info = $"{programName} ·¹º§ {level + 1}";
            NoticeManager.AddNotice(image.sprite, info);
            isNotice = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
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
            coolImage.fillAmount = 1 - (1.0f / coolTime);
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

    protected virtual void ChildLevelUp()
    {

    }

    protected abstract void ExecuteProgram();
}
