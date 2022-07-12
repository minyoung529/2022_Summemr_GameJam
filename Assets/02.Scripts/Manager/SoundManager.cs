using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource clickSource;
    [SerializeField]
    AudioClip clickSound;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClickSound();
        }
    }
    void OnClickSound()
    {
        clickSource.PlayOneShot(clickSound);
    }
}
