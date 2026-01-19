using UnityEngine;

public class Mission : MonoBehaviour
{
    public bool autoTurnOnWhenUnlocked = false;
    public bool oneTimeTrigger = true;

    [Header("Save Key")]
    public string missionDestroyedKey = "Mission_A_Destroyed";

    private bool activated = false;

    void Start()
    {
        if (PlayerPrefs.GetInt(missionDestroyedKey, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (oneTimeTrigger && activated) return;

        ToggleController toggleController = FindObjectOfType<ToggleController>();
        if (toggleController != null)
        {
            toggleController.UnlockToggle();

            if (autoTurnOnWhenUnlocked)
                toggleController.targetToggle.isOn = true;
        }

        activated = true;

        PlayerPrefs.SetInt(missionDestroyedKey, 1);
        PlayerPrefs.Save();

        Destroy(gameObject);
    }
}
