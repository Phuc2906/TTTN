using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("GameVolume", 1f);

        slider.onValueChanged.AddListener((value) =>
        {
            MusicManager.instance.SetVolume(value);
        });
    }
}
