using UnityEngine;
using System.Collections;

public class HealOverTime : MonoBehaviour
{
    [Header("Canvas cần Heal (kéo vào)")]
    public GameObject targetCanvas;     

    [Header("Player")]
    public PlayerHealth playerHealth;

    [Header("Hồi máu mỗi lần")]
    public int healAmount = 25;

    [Header("Thời gian hồi (giây)")]
    public float duration = 30f;

    [Header("Khoảng cách mỗi lần hồi (giây)")]
    public float healInterval = 1f;

    Coroutine healRoutine;

    private void Update()
    {
        if (targetCanvas != null && targetCanvas.activeSelf)
        {
            if (healRoutine == null)
                healRoutine = StartCoroutine(HealProcess());
        }
        else
        {
            if (healRoutine != null)
            {
                StopCoroutine(healRoutine);
                healRoutine = null;
            }
        }
    }

    IEnumerator HealProcess()
    {
        float timer = duration;

        while (timer > 0 && targetCanvas.activeSelf)
        {
            playerHealth.Heal(healAmount);
            timer -= healInterval;
            yield return new WaitForSeconds(healInterval);
        }

        healRoutine = null;
    }
}
