using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Header("Ŭ����")]
    AudioSource clickSource;
    [SerializeField]
    AudioClip clickSound;


    [SerializeField, Header("�˾�â ȿ����")]
    AudioSource tabSfxSource;
    [SerializeField]
    AudioClip[] tabSfxClip;
    
    [SerializeField, Header("SFX ȿ����")]
    AudioSource sfxSource;
    [SerializeField]
    AudioClip[] sfxClip;

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
    public void TabOpen()
    {
        tabSfxSource.PlayOneShot(tabSfxClip[0]);
    }
    public void TabClose()
    {
        tabSfxSource.PlayOneShot(tabSfxClip[1]);
    }

    public void SfxSoundOn(int i)
    {
        sfxSource.PlayOneShot(sfxClip[i]);
    }
}
