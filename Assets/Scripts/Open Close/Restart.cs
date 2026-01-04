using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [Header("Tổng số coin / reward ban đầu")]
    public int totalCoins = 48;

    [Header("Tổng số Enemy ban đầu")]
    public int totalEnemies = 20;

    [Header("Tổng số nhóm Enemy (Enemy CHA)")]
    public int totalEnemyGroups = 5;
    [Header("Tổng số OpenGO")]
    public int totalOpenGO = 10;

    public void RestartCurrentScene()
    {
        for (int i = 0; i < totalCoins; i++)
        {
            PlayerPrefs.DeleteKey("Coin_" + i);
        }

        for (int i = 0; i < totalEnemies; i++)
        {
            PlayerPrefs.DeleteKey("Enemy_" + i);
            PlayerPrefs.DeleteKey("Enemy_X_" + i);
            PlayerPrefs.DeleteKey("Enemy_Y_" + i);
            PlayerPrefs.DeleteKey("Enemy_Facing_" + i);
        }

        for (int i = 0; i < totalEnemyGroups; i++)
        {
            PlayerPrefs.DeleteKey("EnemyGroup_" + i);
        }

        for (int i = 0; i < totalOpenGO; i++)
        {
        PlayerPrefs.DeleteKey("OpenGO_" + i);
        }

        PlayerPrefs.DeleteKey("PlayerHealth");

        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerFacing");


        PlayerPrefs.Save();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
