using UnityEngine;

public class CheckLevel : MonoBehaviour
{
    public GameObject buttonObject;

    void Start()
    {
        CheckButton();
    }

    void CheckButton()
    {
        bool done =
            PlayerPrefs.GetInt("Easy_Level4", 0) == 1 ||
            PlayerPrefs.GetInt("Normal_Level4", 0) == 1 ||
            PlayerPrefs.GetInt("Hard_Level4", 0) == 1;

        buttonObject.SetActive(done);
    }
}