using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Header("클릭음")]
    AudioSource clickSource;
    [SerializeField]
    AudioClip clickSound;


    [SerializeField, Header("팝업창 효과음")]
    AudioSource tabSfxSource;
    [SerializeField]
    AudioClip[] tabSfxClip;
    
    [SerializeField, Header("SFX 효과음")]
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
