using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mail : MonoBehaviour
{
    private string address;
    private const int MAX_ADDRESS_COUNT = 3;

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
    private ParticleSystem[] particles = new ParticleSystem[MAX_ADDRESS_COUNT];
    public Color[] color;

    public MailIcon _iconM;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        for (int i = 0; i < MAX_ADDRESS_COUNT; ++i)
        {
            text[i] = inputImages[i].GetComponentInChildren<Text>();
            particles[i] = inputImages[i].GetComponentInChildren<ParticleSystem>();
        }
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
                targetPicker.position = Vector3.MoveTowards(targetPicker.transform.position, pos, Time.deltaTime * 40f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                _iconM.OnCoolTime();
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
        if (inputs.Count == 0 || inputs.Count > MAX_ADDRESS_COUNT) return;

        if (inputs[inputs.Count - 1] == address[inputs.Count - 1] - '0')
        {
            SoundManager.Instance.SfxSoundOn(3);
            inputImages[inputs.Count - 1].DOColor(color[inputs.Count - 1], 0.5f).
                OnComplete(() => inputImages[inputs.Count - 1].DOKill());

            particles[inputs.Count - 1].Play();

            if (inputs.Count == MAX_ADDRESS_COUNT)
            {
                isCorrect = true;

                UIManager.Instance.EnactiveWindow(gameObject);
                transform.DOScale(0f, 0.3f).OnComplete(() => transform.DOKill());

                targetPicker.gameObject.SetActive(true);
            }
        }
        else
        {
            SoundManager.Instance.SfxSoundOn(2);
            rectTransform.DOShakeAnchorPos(1f, 10)
                .OnComplete(() => transform.DOScale(0f, 0.3f)
                .OnComplete(() => gameObject.SetActive(false)));
                _iconM.OnCoolTime();
        }
    }

    private void ActVirus()
    {
        SoundManager.Instance.SfxSoundOn(11);
        isCorrect = false;
        targetPicker.gameObject.SetActive(false);

        List<Monster> monsters = GameManager.Instance.monsters;
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

        List<Monster> monsters = GameManager.Instance.monsters;
        monsters = monsters.FindAll(x => x.IsVaccine);

        foreach (Monster monster in monsters)
        {
            monster.Die();
        }

        gameObject.SetActive(false);
    }
}