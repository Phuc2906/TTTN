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

    [Header("Shadows")]
    public int maxShadowCount = 100;

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

         for (int i = 0; i < maxShadowCount; i++)
        {
            PlayerPrefs.DeleteKey("Shadow_" + i);
        }
        
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("Player_Dead");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerFacing");

        PlayerPrefs.DeleteKey("EnemyIndex");

        PlayerPrefs.DeleteKey("HealthWall");
        PlayerPrefs.DeleteKey("HealthWall_Dead");

        PlayerPrefs.DeleteKey("HealthRuby");

        PlayerPrefs.DeleteKey("Room_01Canvas");
        PlayerPrefs.DeleteKey("Room_02Canvas");
        PlayerPrefs.DeleteKey("Room_03Canvas");
        PlayerPrefs.DeleteKey("Room_04Canvas");
        PlayerPrefs.DeleteKey("Room_05Canvas");
        PlayerPrefs.DeleteKey("Room_06Canvas");
        PlayerPrefs.DeleteKey("Room_07Canvas");
        PlayerPrefs.DeleteKey("Room_08Canvas");
        PlayerPrefs.DeleteKey("Room_09Canvas");
        PlayerPrefs.DeleteKey("Room_10Canvas");
        PlayerPrefs.DeleteKey("Room_11Canvas");
        PlayerPrefs.DeleteKey("Room_12Canvas");
        PlayerPrefs.DeleteKey("Room_13Canvas");
        PlayerPrefs.DeleteKey("Room_14Canvas");
        PlayerPrefs.DeleteKey("Room_15Canvas");
        PlayerPrefs.DeleteKey("Room_16Canvas");
        PlayerPrefs.DeleteKey("Room_17Canvas");
        PlayerPrefs.DeleteKey("Room_18Canvas");
        PlayerPrefs.DeleteKey("Room_19Canvas");
        PlayerPrefs.DeleteKey("Room_20Canvas");
        PlayerPrefs.DeleteKey("Room_21Canvas");

        PlayerPrefs.DeleteKey("NPC_A_Destroyed");
        PlayerPrefs.DeleteKey("Mission_A_Destroyed");
        PlayerPrefs.DeleteKey("Toggle_Unlocked");

        PlayerPrefs.DeleteKey("NPC_01_Destroyed");

        PlayerPrefs.DeleteKey("TeammateDead");
        PlayerPrefs.DeleteKey("TeammateHealth");

        foreach (var tm in FindObjectsOfType<TeammateMove>())
        tm.DeleteSave();

        
        PlayerPrefs.DeleteKey("Rent_NPC_01");

        PlayerPrefs.Save();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
