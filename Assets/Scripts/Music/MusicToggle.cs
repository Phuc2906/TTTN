using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        toggle.isOn = PlayerPrefs.GetInt("MusicState", 1) == 1;

        toggle.onValueChanged.AddListener((isOn) =>
        {
            MusicManager.instance.SetMusic(isOn);
        });
    }
}
