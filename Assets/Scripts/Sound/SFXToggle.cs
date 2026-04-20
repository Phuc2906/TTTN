using UnityEngine;
using UnityEngine.UI;

public class SFXToggle : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        toggle.isOn = PlayerPrefs.GetInt("SFXState", 1) == 1;

        toggle.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.instance.SetSFX(isOn);
        });
    }
}