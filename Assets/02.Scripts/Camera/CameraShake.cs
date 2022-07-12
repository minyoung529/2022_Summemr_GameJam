using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    public Camera mainCam;
    Vector3 cameraPos;

    [SerializeField][Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField][Range(0.1f, 1f)] float duration = 0.5f;

    public void Shake()
    {
        cameraPos = mainCam.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }

    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosZ = Random.value * shakeRange * 2 - shakeRange;

        Vector3 camPos = mainCam.transform.position;
        camPos.x+=cameraPosX;
        camPos.z+=cameraPosZ;
        mainCam.transform.position=camPos;
    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        mainCam.transform.position = cameraPos;
    }
}
