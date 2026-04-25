using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Admin : MonoBehaviour
{
    [Header("Players")]
    public List<PlayerHealth> healthPlayers;
    public List<PlayerExpManager> expPlayers;

    private PlayerHealth currentHealthPlayer;
    private PlayerExpManager currentExpPlayer;

    [Header("Input")]
    public TMP_InputField healthInput;
    public TMP_InputField levelInput;
    public TMP_InputField damageInput;

    [Header("Warning")]
    public GameObject warningCanvas;

    [Header("Exp Canvas")]
    public GameObject expCanvas;

    [Header("Health Canvas")]
    public GameObject healthCanvas;

    [Header("Canvas")]
    public GameObject damageCanvas;

    void Update()
    {
        currentHealthPlayer = null;
        foreach (var p in healthPlayers)
        {
            if (p != null && p.gameObject.activeInHierarchy)
            {
                currentHealthPlayer = p;
                break;
            }
        }

        currentExpPlayer = null;
        foreach (var p in expPlayers)
        {
            if (p != null && p.gameObject.activeInHierarchy)
            {
                currentExpPlayer = p;
                break;
            }
        }
    }

    public void SetHealth()
    {
        if (healthInput == null || currentHealthPlayer == null) return;

        if (int.TryParse(healthInput.text, out int value))
        {
            if (value <= 0)
            {
                if (warningCanvas != null)
                    warningCanvas.SetActive(true);
                return;
            }

            currentHealthPlayer.SetHealth(value);
            healthCanvas.SetActive(false);
        }
    }

    public void SetLevel()
    {
        if (levelInput == null || currentExpPlayer == null) return;

        if (int.TryParse(levelInput.text, out int value))
        {
            if (value <= 0)
            {
                if (warningCanvas != null)
                    warningCanvas.SetActive(true);
                return;
            }
            currentExpPlayer.SetLevel(value);
            expCanvas.SetActive(false);
        }
    }

    public void SetDamage()
    {
        if (int.TryParse(damageInput.text, out int value))
        {
            if (value <= 0)
            {
                if (warningCanvas != null)
                    warningCanvas.SetActive(true);
                return;
            }
            DamageManager.instance.SetDamage(value);
            damageCanvas.SetActive(false);
        }
    }

    public void HideWarning()
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }
}