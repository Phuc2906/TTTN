using UnityEngine;
using System.Collections.Generic;

public class CarPet : MonoBehaviour
{
    [Header("Canvas sẽ bật khi chạm")]
    public List<GameObject> canvasToShow;

    private bool hasTriggered = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return; 

        if (other.CompareTag("Player"))
        {
            foreach (GameObject canvas in canvasToShow)
            {
                if (canvas != null)
                    canvas.SetActive(true);
            }
            hasTriggered = true; 
        }
    }
}
