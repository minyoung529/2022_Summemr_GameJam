using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HeartButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private HeartAttack heartAttack;
    [SerializeField] private ParticleSystem heartParticle;
    private List<HeartAttack> heartAttacks = new List<HeartAttack>();

    private Camera mainCam;

    private int heartCount = 0;
    public int emojiCount = 3;
    private const int MAX_HEART_COUNT = 3;

    private Image image;
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Transform instagramWindow;

    private void Start()
    {
        mainCam = Camera.main;
        image = GetComponent<Image>();
        emojiCount = 3;
    }

    private void OnEnable()
    {
        heartCount = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        heartParticle.gameObject.SetActive(true);
        if (heartCount == MAX_HEART_COUNT)
        {
            image.sprite = buttonSprites[1];
        }

        if (heartCount > MAX_HEART_COUNT)
        {
        SoundManager.Instance.SfxSoundOn(7);
            StartCoroutine(ExplosionHearts());
        }
        else
        {
        SoundManager.Instance.SfxSoundOn(6);
            GenerateHeart();
        }
    }

    private void GenerateHeart()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red, 1f);

        if (Physics.Raycast(ray, 100f))
        {
            for (int i = 0; i < emojiCount; ++i)
            {
                HeartAttack heart = PoolManager.Instance.Pop(heartAttack) as HeartAttack;

                Vector3 pos = transform.position;
                pos.x += Random.Range(-1f, 1f);
                pos.z += Random.Range(-1f, 1f);

                heart.transform.position = pos;

                heart.AppearHeart();
                heartAttacks.Add(heart);
            }

            ++heartCount;
        }
    }

    private IEnumerator ExplosionHearts()
    {
        heartParticle.gameObject.SetActive(false);

        heartCount = 0;
        instagramWindow.DOScale(0f, 0.3f).OnComplete(() => instagramWindow.gameObject.SetActive(false));
        image.sprite = buttonSprites[0];

        Vector3 initPos = new Vector3(-8.8f, 0f, -5f);

        for (int i = 0; i < 3; i++)
        {
            foreach (HeartAttack heart in heartAttacks)
            {
                heart.Explosion(initPos, 7f);
            }

            initPos.x += 8.8f;
            yield return null;
        }


    }
}
