using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text knowledgeText;
    private string loadingText;
    private string knowledgeStr;

    [SerializeField] private float typeDelay;

    private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private AudioClip typingSoundLong;
    [SerializeField] private AudioClip windowSound;
    [SerializeField] private AudioClip shungSound;

    [SerializeField] private MeshRenderer blackScreen;
    [SerializeField] private Material loadingMat;
    [SerializeField] private Transform loadingFrame;
    [SerializeField] private GameObject knowledgePanel;
    [SerializeField] private GameObject startMenu;

    private void Awake()
    {
        startMenu.SetActive(false);
        knowledgePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        TextAsset textFile = Resources.Load("LoadingText") as TextAsset;
        loadingText = textFile.text;
        knowledgeStr =
            "�� ��ǻ���̻��� �����? ��ũ�� �ҹ��ٿ� ���� \n" +
            "�ٵ� ���̷��� ���� ������(�� ���޴� ��ī��; ;)\n" +
            "�̰� ������ �ƺ� ���� ���� ��ǻ�Ͷ� �������� �ʵſ�...�Ф�\n\n" +
            "�� ���⼭ �ٿ��ߴ´� ���� ����; ; ; ; ; ; ; (����" +
            "\nhttps://github.com/namsojeong\n" +
            "���� 100 �帱�����(���� �ȳ� �Ű��)";
    }

    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < loadingText.Length; i++)
        {
            text.text += loadingText[i];
            PlayTypingSoundLong();
            yield return new WaitForSeconds(typeDelay);
        }
        for (int i = 0; i < 3; i++)
        {
            text.text += ".";
            PlayTypingSound();
            yield return new WaitForSeconds(typeDelay * 100);
        }
        text.gameObject.SetActive(false);
        blackScreen.material = loadingMat;

        audioSource.Stop();
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        PlayWindowSound();
        loadingFrame.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        loadingFrame.gameObject.SetActive(false);
        blackScreen.gameObject.SetActive(false);
        knowledgePanel.SetActive(true);
        StartCoroutine(KnowledgeTab());
    }

    IEnumerator KnowledgeTab()
    {
        PlayTypingSoundLong();
        for (int i = 0; i < knowledgeStr.Length; i++)
        {
            knowledgeText.text += knowledgeStr[i];
            yield return new WaitForSeconds(typeDelay*10);
        }
            PlayTypingSoundStop();
        startMenu.SetActive(true);
        StartCoroutine(UpPanel());
    }

    IEnumerator UpPanel()
    {
        PlayShungSound();
        for(int i=0;i<95;i++)
        {
        startMenu.transform.position = new Vector3(startMenu.transform.position.x, startMenu.transform.position.y, startMenu.transform.position.z+0.1f);
        yield return new WaitForSeconds(0.01f);
        }
    }

    private void PlayTypingSoundStop()
    {
        audioSource.Stop();
    }
    private void PlayTypingSoundLong()
    {
        audioSource.clip = typingSoundLong;
        audioSource.loop = true;
        audioSource.Play();
    }
    private void PlayTypingSound()
    {
        audioSource.clip = typingSound;
        audioSource.loop = false;
        audioSource.Play();
    }

    private void PlayWindowSound()
    {
        audioSource.clip = windowSound;
        audioSource.Play();
    }
    
    private void PlayShungSound()
    {
        audioSource.clip = shungSound;
        audioSource.loop = false;
        audioSource.Play();
    }
}
