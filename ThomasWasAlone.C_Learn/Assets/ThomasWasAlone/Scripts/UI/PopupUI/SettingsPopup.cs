using Common.EnumExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : BasePopupUI
{
    [SerializeField] private AudioClip _btnClip;
    [SerializeField] private Slider masterSlider, bgmSlider, sfxSlider;

    
    /// <summary>
    /// 저장된 PlayerPrefs를 불러와서 초기 세팅
    /// </summary>
    public override void Init<T>(T option)
    {
        base.Init(option);

        masterSlider.value = PlayerPrefs.GetFloat(SoundType.Master.EnumToString());
        bgmSlider.value = PlayerPrefs.GetFloat(SoundType.BGM.EnumToString());
        sfxSlider.value = PlayerPrefs.GetFloat(SoundType.SFX.EnumToString());
    }

    
    /// <summary>
    /// 슬라이더를 이용하여 AudioMixer의 파라미터 값 변경
    /// </summary>
    public void OnMasterSliderChanged()
    {
        Managers.Sound.SetVolume(SoundType.Master, masterSlider.value);
    }

    public void OnBGMSliderChanged()
    {
        Managers.Sound.SetVolume(SoundType.BGM, bgmSlider.value);
    }

    public void OnSFXSliderChanged()
    {
        Managers.Sound.SetVolume(SoundType.SFX, sfxSlider.value);
    }


    
    /// <summary>
    /// 해당 팝업 비활성화
    /// </summary>
    public void OnClickReturnButton()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }
}