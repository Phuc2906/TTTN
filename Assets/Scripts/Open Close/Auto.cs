using UnityEngine;
using System.Collections;

public class Auto : MonoBehaviour
{
    public int shadowID; 
    public GameObject noticeCanvas;
    public float delay = 2f;

    string key;

    void Awake()
    {
        key = "Shadow_" + shadowID;

        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        if (noticeCanvas == null)
            noticeCanvas = gameObject;

        StartCoroutine(CloseAfterDelay());
    }

    IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();

        Destroy(gameObject);
    }
}