using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mail : MonoBehaviour
{
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
    private List<int> inputs = new List<int>();

    public Image[] inputImages;
    private Text[] text = new Text[MAX_ADDRESS_COUNT];
    public Color[] color;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        for (int i = 0; i < MAX_ADDRESS_COUNT; ++i)
        {
            text[i] = inputImages[i].GetComponentInChildren<Text>();
        }

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

                pos.y = 0f;
                targetPicker.position = Vector3.MoveTowards(targetPicker.transform.position, pos, Time.deltaTime * 20f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                ActVirus();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            inputs.Add(0);
            Send();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            inputs.Add(1);
            Send();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            inputs.Add(2);
            Send();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            inputs.Add(3);
            Send();
        }
    }

    private void OnEnable()
    {
        inputs.Clear();

        foreach (Image image in inputImages)
            image.color = new Color32(212, 212, 212, 255);

        SetAdress();
    }

    private void SetAdress()
    {
        address = "";

        for (int i = 0; i < MAX_ADDRESS_COUNT; ++i)
        {
            int rand = Random.Range(0, 4);
            address += rand.ToString();

            switch (rand)
            {
                case 0:
                    text[i].text = "ก่";
                    break;

                case 1:
                    text[i].text = "ก็";
                    break;

                case 2:
                    text[i].text = "ก้";
                    break;

                case 3:
                    text[i].text = "กๆ";
                    break;
            }
        }
    }

    private void Send()
    {
        if (inputs[inputs.Count - 1] == address[inputs.Count - 1] - '0')
        {
            SoundManager.Instance.SfxSoundOn(3);
            inputImages[inputs.Count - 1].DOColor(color[inputs.Count - 1], 0.5f);

            if (inputs.Count == MAX_ADDRESS_COUNT)
            {
                isCorrect = true;
                transform.DOScale(0f, 0.3f);

                targetPicker.gameObject.SetActive(true);
            }
        }
        else
        {
            SoundManager.Instance.SfxSoundOn(2);
            rectTransform.DOShakeAnchorPos(1f, 10)
                .OnComplete(() => transform.DOScale(0f, 0.3f)
                .OnComplete(() => gameObject.SetActive(false)));
        }
    }

    private void ActVirus()
    {
        SoundManager.Instance.SfxSoundOn(11);
        isCorrect = false;
        targetPicker.gameObject.SetActive(false);

        List<Monster> monsters = new List<Monster>(FindObjectsOfType<Monster>());
        monsters = monsters.FindAll(x => Vector3.Distance(x.transform.position, targetPicker.position) <= distance);

        foreach (Monster m in monsters)
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