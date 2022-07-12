using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mail : MonoBehaviour
{
    [SerializeField] private InputField addressField;
    [SerializeField] private Text addressPlaceHolder;
    [SerializeField] private Button sendButton;
    private string address;
    private const int MAX_ADDRESS_COUNT = 5;

    private RectTransform rectTransform;

    [Header("Virus")]
    [SerializeField]
    VirusObject virusPrefab;

    public float delaySecond = 3f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        sendButton.onClick.AddListener(Send);
    }

    private void OnEnable()
    {
        SetAdress();
    }

    private void SetAdress()
    {
        address = "";

        for (int i = 0; i < MAX_ADDRESS_COUNT; ++i)
        {
            if (i % 2 == 0)
                address += (char)Random.Range(65, 91);

            else
                address += (Random.Range(0, 10)).ToString();
        }

        addressPlaceHolder.text = address;
    }

    private void Send()
    {
        if (addressField.text.Trim() == address)
        {
            virusPrefab.gameObject.SetActive(true);
            transform.DOScale(0f, 0.3f);

            StartCoroutine(ExplosionDelay());
        }
        else
        {
            rectTransform.DOShakeAnchorPos(1f, 10);
        }
    }

    private IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(delaySecond * 2f);

        List<Monster> monsters = new List<Monster>(FindObjectsOfType<Monster>());
        monsters = monsters.FindAll(x => x.IsVaccine);

        foreach(Monster monster in monsters)
        {
            monster.Die();
        }

        gameObject.SetActive(false);
    }
}
