using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [Header("Scene hiện tại")]
    public string currentSceneName;
    [Header("Scene so sánh")]
    public string compareSceneName;
    [Header("Scene muốn chuyển đến")]
    public string targetSceneName;

    [Header("Pause")]
    public bool savePauseState = false;

    [Header("Canvases")]
    public List<GameObject> lockCanvases = new List<GameObject>();

    [Header("UI")]
    public GameObject warningCanvas;

    public void LoadTargetScene()
    {
        if (IsAnyLockCanvasActive())
        {
            if (warningCanvas != null)
                warningCanvas.SetActive(true);
            return;
        }

        PlayerPrefs.SetString("LastScene", currentSceneName);

        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        if (playerMove != null)
            playerMove.SavePosition();

        if (savePauseState && currentSceneName == compareSceneName && GameManager.instance != null)
            PlayerPrefs.SetInt("WasPaused", GameManager.instance.IsPaused() ? 1 : 0);
        else
            PlayerPrefs.SetInt("WasPaused", 0);

        PlayerPrefs.Save();

        SceneManager.LoadScene(targetSceneName);
    }

    bool IsAnyLockCanvasActive()
    {
        foreach (var canvas in lockCanvases)
        {
            if (canvas != null && canvas.activeInHierarchy)
                return true;
        }
        return false;
    }
}
