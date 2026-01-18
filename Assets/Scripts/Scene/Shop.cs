using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [Header("Scene hiện tại")]
    public string currentSceneName;

    [Header("Scene muốn chuyển đến")]
    public string targetSceneName;

    [Header("Lưu Pause")]
    public bool savePauseState = false;

    [Header("Players")]
    public List<PlayerHealth> players = new List<PlayerHealth>();

    [Header("UI")]
    public GameObject warningCanvas;

    public void LoadTargetScene()
{
    PlayerHealth activePlayer = GetActivePlayer();

    if (activePlayer != null && !activePlayer.IsHealthFull())
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(true);
        return;
    }

    PlayerPrefs.SetString("LastScene", currentSceneName);

    PlayerMove playerMove = FindObjectOfType<PlayerMove>();
    if (playerMove != null)
        playerMove.SavePosition();

    if (savePauseState && currentSceneName == "Easy_Level1" && GameManager.instance != null)
        PlayerPrefs.SetInt("WasPaused", GameManager.instance.IsPaused() ? 1 : 0);
    else
        PlayerPrefs.SetInt("WasPaused", 0);

    PlayerPrefs.Save();

    SceneManager.LoadScene(targetSceneName);
}


    PlayerHealth GetActivePlayer()
    {
        foreach (var p in players)
        {
            if (p != null && p.gameObject.activeInHierarchy)
                return p;
        }

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go)
            return go.GetComponent<PlayerHealth>();

        return null;
    }
}
