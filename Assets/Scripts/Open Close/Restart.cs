using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [Header("Enemies")]
    public int totalEnemies = 20;

    [Header("Enemy Groups")]
    public int totalEnemyGroups = 5;

    [Header("Total OpenGO")]
    public int totalOpenGO = 10;

    [Header("Total Box")]
    public int totalBox = 5;

    public void RestartCurrentScene()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            PlayerPrefs.DeleteKey("Enemy_" + i);
            PlayerPrefs.DeleteKey("Enemy_X_" + i);
            PlayerPrefs.DeleteKey("Enemy_Y_" + i);
            PlayerPrefs.DeleteKey("Enemy_Facing_" + i);
            PlayerPrefs.DeleteKey("EnemyHealth_" + i);
        }

        for (int i = 0; i < totalEnemyGroups; i++)
        {
            PlayerPrefs.DeleteKey("EnemyGroup_" + i);
        }

        for (int i = 0; i < totalOpenGO; i++)
        {
            PlayerPrefs.DeleteKey("OpenGO_" + i);
        }

        for (int i = 0; i < totalBox; i++)
        {
            PlayerPrefs.DeleteKey("Box_" + i);
        }
        
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerFacing");

        PlayerPrefs.DeleteKey("HealthWall");
        PlayerPrefs.DeleteKey("HealthWall_Dead");

        PlayerPrefs.DeleteKey("HealthRuby");

        PlayerPrefs.DeleteKey("NPC_A_Destroyed");
        PlayerPrefs.DeleteKey("Mission_A_Destroyed");
        PlayerPrefs.DeleteKey("Toggle_Unlocked");

        PlayerPrefs.DeleteKey("NPC_01_Destroyed");

        PlayerPrefs.DeleteKey("TeammateDead");
        PlayerPrefs.DeleteKey("TeammateHealth");

        PlayerPrefs.Save();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
