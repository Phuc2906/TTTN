using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle targetToggle;
    public bool isUnlocked = false; 

    void Start()
    {
        targetToggle.isOn = false;      
        targetToggle.interactable = false; 
    }

    public void UnlockToggle() 
    {
        isUnlocked = true;
        targetToggle.interactable = true;
        Debug.Log("Toggle đã được mở khóa!");
    }
}
