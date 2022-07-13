using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    AudioSource startAudio;
    [SerializeField]
    MeshRenderer startMat;
    [SerializeField]
    GameObject canvas;

    public void StartGame()
    {
        startMat.material.color = Color.cyan;
        canvas.SetActive(false);
        startAudio.Play();
        Invoke("GoMain", 2f);
    }
    void GoMain()
    {
        UIManager.Instance.SceneChange("Main");
    }
}
