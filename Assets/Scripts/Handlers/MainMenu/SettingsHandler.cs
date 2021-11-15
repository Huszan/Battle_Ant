
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
    public void Submit()
    {
        GameObject.Find("GlobalManagers").
            GetComponent<SettingsManager>().
            SaveSetting(GetSettings());
    }
    private Setting GetSettings()
    {
        Setting setting = new Setting();

        setting.volume = new Volume(
            sliderVolumeMaster.value,
            sliderVolumeSfx.value,
            sliderVolumeMusic.value);
        setting.resolution = Screen.resolutions[dropdownResolution.value];
        setting.fullscreen = toggleFullscren.isOn;

        return setting;
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
