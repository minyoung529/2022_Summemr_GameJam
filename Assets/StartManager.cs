using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    AudioSource startAudio;
    [SerializeField]
    GameObject VV;
    [SerializeField]
    GameObject canvas;

    public Image image;
    public Sprite[] helpSprites;
    public Button[] helpButtons;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void StartGame()
    {
        canvas.SetActive(false);
        VV.SetActive(true);
        Invoke("GoMain", 0.7f);
    }

    void GoMain()
    {
        UIManager.Instance.SceneChange("Main");
    }

    public void NextButton()
    {
        helpButtons[0].gameObject.SetActive(true);
        helpButtons[1].gameObject.SetActive(false);
        image.sprite = helpSprites[1];
    }

    public void PreviousButton()
    {
        helpButtons[0].gameObject.SetActive(false);
        helpButtons[1].gameObject.SetActive(true);
        image.sprite = helpSprites[0];
    }
}
