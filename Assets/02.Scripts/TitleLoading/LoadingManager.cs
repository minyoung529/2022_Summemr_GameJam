using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Text text;
    private string loadingText;

    [SerializeField] private float typeDelay;

    private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private AudioClip windowSound;

    [SerializeField] private MeshRenderer blackScreen;
    [SerializeField] private Material loadingMat;
    [SerializeField] private Transform loadingFrame;
    [SerializeField] private GameObject startMenu;
    private void Awake()
    {
        startMenu.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        TextAsset textFile = Resources.Load("LoadingText") as TextAsset;
        loadingText = textFile.text;
    }

    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        for(int i = 0; i < loadingText.Length; i++)
        {
            text.text += loadingText[i];
            PlayTypingSound();
            yield return new WaitForSeconds(typeDelay);
        }
        for(int i = 0; i < 3; i++)
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
        startMenu.SetActive(true);
    }

    private void PlayTypingSound()
    {
        audioSource.clip = typingSound;
        audioSource.Play();
    }
    
    private void PlayWindowSound()
    {
        audioSource.clip = windowSound;
        audioSource.Play();
    }
}
