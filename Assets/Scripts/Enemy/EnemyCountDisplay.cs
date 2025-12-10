using UnityEngine;
using TMPro;

public class EnemyCountDisplay : MonoBehaviour
{
    public TMP_Text countText;  

    public void UpdateCount(int alive, int maxAlive, int spawned, int spawnLimit)
    {
        if (countText != null)
            countText.text = $"{spawned}/{spawnLimit}";
    }
}
