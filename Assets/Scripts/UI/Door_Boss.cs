using UnityEngine;

public class Door_Boss : MonoBehaviour
{
    [Header("Level Requirement")]
    public int requiredLevel = 5;

    [Header("Canvas Thông báo nếu level chưa đủ")]
    public GameObject notEnoughLevelCanvas;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int playerLevel = PlayerExpManager.Instance.GetLevel();

            if (playerLevel >= requiredLevel)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (notEnoughLevelCanvas != null)
                    notEnoughLevelCanvas.SetActive(true);

                    Time.timeScale = 0f;
            }
        }
    }
}
