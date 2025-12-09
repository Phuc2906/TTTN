using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject lockCanvas; 

    void Start()
    {
        lockCanvas.SetActive(false);
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Easy_Level1");
    }

    public void PlayLevel2()
    {
        int level1Done = PlayerPrefs.GetInt("Level1Clear", 0);

        if(level1Done == 1)
        {
            SceneManager.LoadScene("Easy_Level2");
        }
        else
        {
            lockCanvas.SetActive(true);
        }
    }
    public void CloseLockCanvas()
    {
        lockCanvas.SetActive(false);
    }
}
