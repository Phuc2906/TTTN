using UnityEngine;
using UnityEngine.UI;

public class ToggleSave : MonoBehaviour
{
    public Toggle toggle;
    public GameObject targetObject;

    public string key = "MyToggleState"; 

    void Start()
    {
        bool savedState = PlayerPrefs.GetInt(key, 0) == 1;

        toggle.isOn = savedState;
        targetObject.SetActive(savedState);

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        targetObject.SetActive(isOn);

        PlayerPrefs.SetInt(key, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}