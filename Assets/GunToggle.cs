using UnityEngine;
using UnityEngine.UI;

public class GunToggle : MonoBehaviour
{
    public Toggle autoFireToggle;

    private const string PREF_KEY = "AUTO_FIRE_MODE";

    void Start()
    {
        bool isAuto = PlayerPrefs.GetInt(PREF_KEY, 1) == 1;

        autoFireToggle.isOn = isAuto;

        ApplyMode(isAuto);

        autoFireToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool value)
    {
        ApplyMode(value);

        PlayerPrefs.SetInt(PREF_KEY, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    void ApplyMode(bool isAuto)
    {
        Gun[] allGuns = FindObjectsOfType<Gun>();

        foreach (Gun g in allGuns)
        {
            if (g != null)
                g.SetFireMode(isAuto);
        }
    }
}