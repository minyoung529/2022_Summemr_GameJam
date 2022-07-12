using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HeartButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private HeartAttack heartAttack;
    private List<HeartAttack> heartAttacks = new List<HeartAttack>();

    private Camera mainCam;

    private int heartCount = 0;
    private const int MAX_HEART_COUNT = 5;

    private Image image;
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Transform instagramWindow;

    private void Start()
    {
        mainCam = Camera.main;
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        heartCount = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (heartCount == MAX_HEART_COUNT)
        {
            image.sprite = buttonSprites[1];
        }

        if (heartCount > MAX_HEART_COUNT)
        {
            StartCoroutine(ExplosionHearts());
        }
        else
        {
            GenerateHeart();
        }
    }

    private void GenerateHeart()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red, 1f);

        if (Physics.Raycast(ray, 100f))
        {
            for (int i = 0; i < 6; ++i)
            {
                HeartAttack heart = PoolManager.Instance.Pop(heartAttack) as HeartAttack;
                heart.AppearHeart();
                heartAttacks.Add(heart);
            }

            ++heartCount;
        }
    }

    private IEnumerator ExplosionHearts()
    {
        heartCount = 0;
        instagramWindow.DOScale(0f, 0.3f).OnComplete(() => instagramWindow.gameObject.SetActive(false));
        image.sprite = buttonSprites[0];

        Vector3 initPos = new Vector3(-7f, 0f, -5f);

        for (int i = 0; i < 3; i++)
        {
            foreach (HeartAttack heart in heartAttacks)
            {
                heart.Explosion(initPos, 7f);
            }

            initPos.x += 7f;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
