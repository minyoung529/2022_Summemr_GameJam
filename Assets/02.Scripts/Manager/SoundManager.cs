using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Header("Ŭ����")]
    AudioSource clickSource;
    [SerializeField]
    AudioClip clickSound;

    [SerializeField, Header("���� ȿ����")]
    AudioSource monsterAudio;
    [SerializeField]
    AudioClip[] monsterSFXClip;

    [SerializeField, Header("�˾�â ȿ����")]
    AudioSource tabSfxSource;
    [SerializeField]
    AudioClip[] tabSfxClip;


    // 0 : Paint ���ݽ� ȿ����
    // 1 : ���� ���� �� ������ �˴ٿ� ȿ����
    // 2 : ���� �Ǽ� ���� ��
    // 3 : ���� ���� ���� ��
    // 4 : ������ �� ��
    // 5 : ���α׷� �� ��
    // 6 : �ν�Ÿ ���ƿ� ���� ��
    // 7 : �ν�Ÿ ���ƿ� Send ���� ��
    // 8 : ������ ������ ȿ����
    // 9 : ������ ������ ȿ����
    // 10 : ȭ�� �μ����� ȿ����
    // 11 : ���� ���̷��� ����߸��� ȿ����
    // 12 : ũ�� ���� ȿ����
    // 13 : �ǰ��� ȿ����


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

    public void OverSound()
    {
        // 3
        sfxSource.PlayOneShot(sfxClip[3]);
    }

    public void ProgramOpen()
    {
        sfxSource.PlayOneShot(sfxClip[5]);
    }

    public void MonsterDieSound()
    {
        monsterAudio.PlayOneShot(monsterSFXClip[0]);
    }
    public void MonsterDamageSound()
    {
        monsterAudio.PlayOneShot(monsterSFXClip[1]);
    }
}
