using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    AudioSource startAudio;
    [SerializeField]
    GameObject VV;
    [SerializeField]
    GameObject canvas;

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
}
