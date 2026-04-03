using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject Canvas_A;
    public GameObject Canvas_B;

    public void LoadReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Canvas_A.SetActive(false);
        Canvas_B.SetActive(true);

        Level[] levels = FindObjectsOfType<Level>();
        foreach (Level lvl in levels)
        {
            lvl.Refresh();
        }

        ModeManager mode = FindObjectOfType<ModeManager>();
        if (mode != null)
            mode.Refresh();
    }
}
