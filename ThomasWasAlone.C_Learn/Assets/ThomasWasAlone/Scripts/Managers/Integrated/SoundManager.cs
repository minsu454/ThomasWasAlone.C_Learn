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
    private ObjectPool soundPool;

    private AudioMixer audioMixer;
    private AudioSource bgmSource;

    public void Init()
    {
        audioMixer = Resources.Load<AudioMixer>(SoundPath.AudioMixerPath);

        CreateAudioSource(SoundType.BGM.EnumToString());
        CreateSoundPool();

        SceneManagerEx.OnLoadCompleted(OnSceneLoaded);
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
    /// <exception cref="NotImplementedException"></exception>
    private void CreateSoundPool()
    {
        GameObject prefab = Resources.Load<GameObject>(SoundPath.SoundPlayerPath);
        soundPool = new ObjectPool(prefab.name, prefab, transform, SoundPath.SoundPlayerCount);
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

    public void SetVolume(SoundType type, float volume)
    {
        audioMixer.SetFloat(type.EnumToString(), Mathf.Log10(volume) * 20);
    }
}
