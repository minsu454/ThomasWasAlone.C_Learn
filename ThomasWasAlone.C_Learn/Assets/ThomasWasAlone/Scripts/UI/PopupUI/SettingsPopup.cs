using Common.EnumExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : BasePopupUI
{
    
    [SerializeField] private Slider masterSlider, bgmSlider, sfxSlider;

    public override void Init<T>(T option)
    {
        base.Init(option);

        masterSlider.value = PlayerPrefs.GetFloat(SoundType.Master.EnumToString());
        bgmSlider.value = PlayerPrefs.GetFloat(SoundType.BGM.EnumToString());
        sfxSlider.value = PlayerPrefs.GetFloat(SoundType.SFX.EnumToString());
    }

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
}