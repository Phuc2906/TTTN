using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenGO : MonoBehaviour
{
    [Header("Save")]
    public int openID;

    [Header("Danh sách GameObject")]
    public List<GameObject> doors;

    [Header("Chỉ số bắt đầu delay")]
    public int delayStartIndex = 1;
    public float delayTime = 0.5f;

    private bool activated = false;
    private string saveKey;

    void Start()
    {
        saveKey = "OpenGO_" + openID;

        activated = PlayerPrefs.GetInt(saveKey, 0) == 1;

        if (activated)
        {
            foreach (GameObject door in doors)
            {
                if (door != null)
                    door.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (!other.CompareTag("Player")) return;

        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i] == null) continue;

            if (i < delayStartIndex)
            {
                doors[i].SetActive(true);
            }
            else
            {
                StartCoroutine(ActivateWithDelay(doors[i], delayTime));
            }
        }

        activated = true;

        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();
    }

    private IEnumerator ActivateWithDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
            obj.SetActive(true);
    }
}
