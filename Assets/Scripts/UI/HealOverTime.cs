using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealOverTime : MonoBehaviour
{
    [Header("Canvas cần Heal")]
    public GameObject targetCanvas;

    [Header("Player")]
    public List<PlayerHealth> players = new List<PlayerHealth>();

    [Header("Heal")]
    public int healAmount = 25;
    public float duration = 30f;
    public float healInterval = 1f;

    Coroutine healRoutine;
    PlayerHealth currentPlayer;

    void Update()
    {
        if (targetCanvas != null && targetCanvas.activeSelf)
        {
            if (healRoutine == null)
            {
                FindActivePlayer();
                if (currentPlayer != null)
                    healRoutine = StartCoroutine(HealProcess());
            }
        }
        else
        {
            StopHeal();
        }
    }

    void FindActivePlayer()
    {
        foreach (var p in players)
        {
            if (p != null && p.gameObject.activeInHierarchy)
            {
                currentPlayer = p;
                return;
            }
        }

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj)
            currentPlayer = obj.GetComponent<PlayerHealth>();
    }

    void StopHeal()
    {
        if (healRoutine != null)
        {
            StopCoroutine(healRoutine);
            healRoutine = null;
        }
    }

    IEnumerator HealProcess()
    {
        float timer = duration;

        while (timer > 0 && targetCanvas.activeSelf && currentPlayer != null)
        {
            currentPlayer.Heal(healAmount);
            timer -= healInterval;
            yield return new WaitForSeconds(healInterval);
        }

        healRoutine = null;
    }
}
