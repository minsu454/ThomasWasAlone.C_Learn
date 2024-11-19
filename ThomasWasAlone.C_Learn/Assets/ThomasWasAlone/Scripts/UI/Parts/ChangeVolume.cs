using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    private AudioMixer myMixer;

    [SerializeField] private Slider masterSlider, bgmSlider, sfxSlider;

    
    public void Init()
    {
        myMixer = Resources.Load<AudioMixer>($"Assets/AudioAssets/AudioMixer");
        
        masterSlider.value = PlayerPrefs.GetFloat("Master", 0.5f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.5f);
        
    }
    

    private void Start()
    {
        SetVolume(masterSlider, "Master");
        SetVolume(bgmSlider, "BGM");
        SetVolume(sfxSlider, "SFX");
    }
    

    public void OnMasterSliderChanged()
    {
        SetVolume(masterSlider, "Master");
    }

    public void OnMusicSliderChanged()
    {
        SetVolume(bgmSlider, "BGM");
    }

    public void OnSFXSliderChanged()
    {
        SetVolume(sfxSlider, "SFX");
    }
    
    
    private void SetVolume(Slider slider, string mixerParametersName)
    {
        float volume = Mathf.Clamp(slider.value, 0.001f, 1.0f);
        myMixer.SetFloat(mixerParametersName, Mathf.Log10(volume) * 20);
    }

    
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
        PlayerPrefs.Save();
    }
}