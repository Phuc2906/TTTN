using UnityEngine;

public class Mission : MonoBehaviour
{
    [Header("Cấu hình")]
    public bool autoTurnOnWhenUnlocked = false;  
    public bool oneTimeTrigger = true;          

    private bool activated = false;            

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (oneTimeTrigger && activated) return; 

        ToggleController toggleController = FindObjectOfType<ToggleController>();

        if (toggleController != null)
        {
            toggleController.UnlockToggle();     

            if (autoTurnOnWhenUnlocked)
            {
                toggleController.targetToggle.isOn = true;
            }
        }

        activated = true;

        Destroy(gameObject);
    }
}
