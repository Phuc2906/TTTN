using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle targetToggle;
    public bool isUnlocked = false;

    [Header("Save Key")]
    public string unlockKey = "Toggle_Unlocked";

    void Start()
    {
        isUnlocked = PlayerPrefs.GetInt(unlockKey, 0) == 1;

        targetToggle.isOn = false;           
        targetToggle.interactable = isUnlocked;
    }

    public void UnlockToggle()
    {
        isUnlocked = true;

        PlayerPrefs.SetInt(unlockKey, 1);
        PlayerPrefs.Save();

        targetToggle.interactable = true;
    }
}
