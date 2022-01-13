using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Slider sliderVolumeMaster;
    [SerializeField]
    private Slider sliderVolumeSfx;
    [SerializeField]
    private Slider sliderVolumeMusic;
    [SerializeField]
    private TMP_Dropdown dropdownResolution;
    [SerializeField]
    private Toggle toggleFullscren;

    private void Awake()
    {
        GetSupportedResolutions();
    }
    private void OnEnable()
    {
        UpdateSettings();
    }
    public void Submit()
    {
        Setting newSetting = GetSettings();

        CurrentUser.user.setting = newSetting;
        SettingsManager.Instance.SaveSetting(newSetting);
        DatabaseManager.UpdateUser(CurrentUser.user);
    }
    private Setting GetSettings()
    {
        Setting setting = new Setting();

        setting.volume = new Volume(
            sliderVolumeMaster.value,
            sliderVolumeSfx.value,
            sliderVolumeMusic.value);
        setting.SetResolution(
            Screen.resolutions[dropdownResolution.value].width,
            Screen.resolutions[dropdownResolution.value].height,
            toggleFullscren.isOn,
            Screen.resolutions[dropdownResolution.value].refreshRate);

        return setting;
    }
    public void UpdateSettings()
    {
        UpdateSoundSettings();
        UpdateScreenSettings();
    }
    private void UpdateSoundSettings()
    {
        // TODO
    }
    private void UpdateScreenSettings()
    {
        var index = 0;
        foreach (var option in dropdownResolution.options)
        {
            if (Screen.resolutions[index].width == Screen.currentResolution.width &&
                Screen.resolutions[index].height == Screen.currentResolution.height)
            {
                dropdownResolution.value = index;
            }
            index++;
        }
        toggleFullscren.isOn = Screen.fullScreen;
    }
 

    private void GetSupportedResolutions()
    {
        dropdownResolution.ClearOptions();
        foreach (Resolution r in Screen.resolutions)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = r.ToString();
            dropdownResolution.options.Add(optionData);
        }
    }
}
