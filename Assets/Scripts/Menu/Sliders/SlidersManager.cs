using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlidersManager : MonoBehaviour
{

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;

    [SerializeField] private TMP_Text musicTextValue;
    [SerializeField] private TMP_Text effectTextValue;

    private float musicValuePercent;
    private float effectValuePercent;

    void Start()
    {
        musicSlider.value = AudioManagerScript.Instance.musicVolume;
        effectSlider.value = AudioManagerScript.Instance.effectVolume;

        ChangeTextValue();
    }

    
    void Update()
    {
        CheckVlaues();
    }

    private void CheckVlaues()
    {
        musicSlider.onValueChanged.AddListener(ChangeVolumeMusic);
        effectSlider.onValueChanged.AddListener(ChangeVolumeEffect);

        void ChangeVolumeMusic(float value)
        {
            AudioManagerScript.Instance.ChangeMusicVolume(value);
            ChangeTextValue();
        }
        void ChangeVolumeEffect(float value)
        {
            AudioManagerScript.Instance.ChangeEffectVolume(value);
            ChangeTextValue();
        }
    }

    private void ChangeTextValue()
    {
        musicValuePercent = (float)System.Math.Round(musicSlider.value, 2) * 100;
        effectValuePercent = (float)System.Math.Round(effectSlider.value, 2) * 100;

        musicTextValue.SetText(musicValuePercent.ToString());
        effectTextValue.SetText(effectValuePercent.ToString());
    }


}
