using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public void LoadChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
    }
}