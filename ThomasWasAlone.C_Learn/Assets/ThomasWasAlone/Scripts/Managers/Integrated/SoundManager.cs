using Common.EnumExtensions;
using Common.Path;
using Common.Pool;
using Common.SceneEx;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour, IInit
{
    private ObjectPool<SoundPlayer> soundPool;

    private AudioMixer audioMixer;
    private AudioSource bgmSource;

    public void Init()
    {
        audioMixer = Resources.Load<AudioMixer>(SoundPath.AudioMixerPath);

        CreateAudioSource(SoundType.BGM.EnumToString());
        CreateSoundPool();

        SceneManagerEx.OnLoadCompleted(OnSceneLoaded);
    }

    public void OnStart()
    {
        InitPlayerPrefsVolume();
    }

    /// <summary>
    /// 씬 로드시 bgm깔아주는 이벤트 함수
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clip = Resources.Load<AudioClip>($"{SoundPath.BGMPath}/{scene.name}");

        if (clip == null)
        {
            Debug.LogWarning($"Is Not BGM AudioClip : {scene.name}");
            return;
        }

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    /// <summary>
    /// 저장된 사운드 크기로 초기화 함수
    /// </summary>
    private void InitPlayerPrefsVolume()
    {
        foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
        {
            string name = type.EnumToString();
            if (!GetAudioMixerGroup(name, out var group))
            {
                Debug.LogError($"Is Not Found Group : {name}");
                return;
            }

            SetVolume(type, PlayerPrefs.GetFloat(name));
        }
    }

    /// <summary>
    /// BGMSource 제작 함수
    /// </summary>
    private void CreateAudioSource(string GroupName)
    {
        GameObject bgmGo = new GameObject(GroupName);
        bgmGo.transform.SetParent(transform);

        bgmSource = bgmGo.AddComponent<AudioSource>();

        if (!GetAudioMixerGroup(GroupName, out var bgmGroup))
        {
            Debug.LogError($"Is Not Found AudioMixerGroup : {GroupName}");
        }

        bgmSource.outputAudioMixerGroup = bgmGroup;

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
    }

    /// <summary>
    /// SoundPool 제작 함수
    /// </summary>
    private void CreateSoundPool()
    {
        GameObject prefab = Resources.Load<GameObject>(SoundPath.SoundPlayerPath);
        soundPool = new ObjectPool<SoundPlayer>(prefab.name, prefab, transform, SoundPath.SoundPlayerCount);
    }

    /// <summary>
    /// AudioMixerGroup 반환 함수
    /// </summary>
    private bool GetAudioMixerGroup(string name, out AudioMixerGroup audioMixerGroup)
    {
        var audioMixerGroupArr = audioMixer.FindMatchingGroups(name);

        if (audioMixerGroupArr.Length == 0)
        {
            audioMixerGroup = null;
            return false;
        }

        audioMixerGroup = audioMixerGroupArr[0];
        return true;
    }

    /// <summary>
    /// 2D 플레이 함수(일반 플레이)
    /// </summary>
    public void SFX2DPlay(AudioClip clip)
    {
        SoundPlayer soundPlayer = soundPool.GetObject();
        soundPlayer.SetDelay(clip.length);
        soundPlayer.SetSound2D();
        soundPlayer.gameObject.SetActive(true);

        soundPlayer.Play(clip);
    }

    /// <summary>
    /// 3D 플레이 함수(원근감 사운드)
    /// </summary>
    public void SFX3DPlay(AudioClip clip)
    {
        SoundPlayer soundPlayer = soundPool.GetObject();
        soundPlayer.SetDelay(clip.length);
        soundPlayer.SetSound3D();
        soundPlayer.gameObject.SetActive(true);

        soundPlayer.Play(clip);
    }

    /// <summary>
    /// 소리 크기 설정 함수
    /// </summary>
    public void SetVolume(SoundType type, float volume)
    {
        audioMixer.SetFloat(type.EnumToString(), Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(type.EnumToString(), volume);
    }
}
