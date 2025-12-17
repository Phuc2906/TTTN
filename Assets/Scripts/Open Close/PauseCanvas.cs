using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    private void OnDisable()
    {
        Time.timeScale = 1f;  
    }
}
