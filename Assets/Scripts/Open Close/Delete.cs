using UnityEngine.SceneManagement;
using UnityEngine;

public class Delete : MonoBehaviour
{

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 
    }
}