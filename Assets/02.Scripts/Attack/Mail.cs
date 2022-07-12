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

    public Transform targetPicker;

    private bool isCorrect = false;

    public float distance = 5f;

    public ParticleSystem particle;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        sendButton.onClick.AddListener(Send);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (isCorrect)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                Vector3 pos = hitInfo.point;
                pos.y = 0.2f;
                targetPicker.transform.position = pos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                ActVirus();
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Send();
        }
    }

    private void OnEnable()
    {
        SetAdress();

        addressField.text = "";
        addressField.ActivateInputField();
    }

    private void SetAdress()
    {
        address = "";

        for (int i = 0; i < MAX_ADDRESS_COUNT; ++i)
        {
            //if (i % 2 == 0)
            //    address += (char)Random.Range((int)'A', (int)'Z');

            //else
            address += (Random.Range(0, 10)).ToString();
        }

        addressPlaceHolder.text = address;
    }

    private void Send()
    {
        if (addressField.text.Trim() == address)
        {
            isCorrect = true;
            transform.DOScale(0f, 0.3f);
            targetPicker.gameObject.SetActive(true);
        }
        else
        {
            rectTransform.DOShakeAnchorPos(1f, 10);
        }
    }

    private void ActVirus()
    {
        isCorrect = false;
        targetPicker.gameObject.SetActive(false);

        List<Monster> monsters = new List<Monster>(FindObjectsOfType<Monster>());
        monsters = monsters.FindAll(x => Vector3.Distance(x.transform.position, targetPicker.position) <= distance);

        foreach(Monster m in monsters)
        {
           VirusObject obj = PoolManager.Instance.Pop(virusPrefab) as VirusObject;
            obj.SetTarget(m);
        }


        particle.transform.position = targetPicker.position;
        particle.gameObject.SetActive(true);
        particle.Play();

        virusPrefab.gameObject.SetActive(true);
        StartCoroutine(ExplosionDelay());
    }

    private IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(delaySecond * 2f);

        List<Monster> monsters = new List<Monster>(FindObjectsOfType<Monster>());
        monsters = monsters.FindAll(x => x.IsVaccine);

        foreach (Monster monster in monsters)
        {
            monster.Die();
        }

        gameObject.SetActive(false);
    }
}