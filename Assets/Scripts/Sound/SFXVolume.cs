using UnityEngine;
using UnityEngine.UI;

public class SFXVolume : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        slider.onValueChanged.AddListener((value) =>
        {
            SoundManager.instance.SetVolume(value);
        });
    }
}