using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Header("클릭음")]
    AudioSource clickSource;
    [SerializeField]
    AudioClip clickSound;

    [SerializeField, Header("몬스터 효과음")]
    AudioSource monsterAudio;
    [SerializeField]
    AudioClip[] monsterSFXClip;

    [SerializeField, Header("팝업창 효과음")]
    AudioSource tabSfxSource;
    [SerializeField]
    AudioClip[] tabSfxClip;


    // 0 : Paint 공격시 효과음
    // 1 : 게임 오버 시 윈도우 셧다운 효과음
    // 2 : 메일 실수 했을 때
    // 3 : 메일 성공 했을 때
    // 4 : 휴지통 열 때
    // 5 : 프로그램 열 때
    // 6 : 인스타 좋아요 누를 때
    // 7 : 인스타 좋아요 Send 누를 때
    // 8 : 휴지통 열리는 효과음
    // 9 : 휴지통 닫히는 효과음
    // 10 : 화면 부서지는 효과음
    // 11 : 메일 바이러스 떨어뜨리는 효과음
    // 12 : 크롬 스핀 효과음
    // 13 : 되감기 효과음


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
