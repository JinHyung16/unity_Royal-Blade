using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughGeneric;
public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audioSource;

    //BGM�� InGame�Ѱ��� �迭�� �ȸ���� �׳� ���
    [Header("In Game BGM")]
    [SerializeField] private AudioClip bgmClip;
    private AudioSource bgmSource;

    //EnemySFX�� Enemy�� Pooling������, Manager���� ���� �����Ѵ�.
    [Header("Enemy SFX")]
    [SerializeField] private AudioClip hitSFXClip;
    private AudioSource enemySFXSource;

    /// <summary>
    /// ���� �׸����� ���� Sound ���� �����ϰ�
    /// </summary>
    public enum BGMType
    {
        InGame = 0,
    }

    /// <summary>
    /// ���� Enemy ���������� Sound ���� �����ϰ�
    /// </summary>
    public enum EnemySFXType
    {
        Cat = 0,
    }

    protected override void OnAwake()
    {
        InitAudioManager();
    }

    private void InitAudioManager()
    {
        //BGM ������ AudioSource �����
        GameObject bgmObj = new GameObject("BGM Player");
        bgmObj.transform.parent = this.transform;
        bgmSource = bgmObj.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.playOnAwake = false;
        bgmSource.bypassListenerEffects = true;
        bgmSource.volume = 0.2f;
        bgmSource.mute = true;
        bgmSource.loop = false;

        //BGM ������ AudioSource �����
        GameObject enemySFXObj = new GameObject("EnemySFX Player");
        enemySFXObj.transform.parent = this.transform;
        enemySFXSource = enemySFXObj.AddComponent<AudioSource>();
        enemySFXSource.clip = hitSFXClip;
        enemySFXSource.playOnAwake = false;
        enemySFXSource.bypassListenerEffects = true;
        enemySFXSource.volume = 0.3f;
        enemySFXSource.mute = true;
        enemySFXSource.loop = false;
    }

    public void BGMPlay()
    {
        bgmSource.volume = 0.1f;
        bgmSource.mute = false;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void BGMStop()
    {
        bgmSource.volume = 0.1f;
        bgmSource.mute = true;
        bgmSource.loop = false;
        bgmSource.Stop();
    }

    public void EnemySFXPlay()
    {
        enemySFXSource.volume = 0.8f;
        enemySFXSource.mute = false;
        enemySFXSource.loop = false;
        enemySFXSource.Play();
    }
}
