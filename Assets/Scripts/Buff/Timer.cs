using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Danh sách enemy")]
    public List<GameObject> enemyList = new List<GameObject>();

    [Header("Delay mỗi lần bật")]
    public float delay = 0.5f;

    [Header("Key lưu PlayerPrefs")]
    public string saveKey = "EnemyIndex";

    private int currentIndex = 0;

    void Start()
    {
        currentIndex = PlayerPrefs.GetInt(saveKey, 0);

    
        for (int i = 0; i < currentIndex; i++)
        {
            if (enemyList[i] != null)
                enemyList[i].SetActive(true);
        }

       
        StartCoroutine(ActiveEnemies());
    }

    IEnumerator ActiveEnemies()
    {
        for (int i = currentIndex; i < enemyList.Count; i++)
        {
            if (enemyList[i] != null)
            {
                enemyList[i].SetActive(true);
            }

            currentIndex = i + 1;

            
            PlayerPrefs.SetInt(saveKey, currentIndex);
            PlayerPrefs.Save();

            yield return new WaitForSeconds(delay);
        }
    }
}