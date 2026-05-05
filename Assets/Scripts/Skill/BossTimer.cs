using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    [Header("Danh sách trap")]
    public List<GameObject> trapList = new List<GameObject>();

    [Header("Delay mỗi lần bật")]
    public float delay = 5f;

    void Start()
    {
        StartCoroutine(ActivateRandomTrap());
    }

    IEnumerator ActivateRandomTrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            int i = Random.Range(0, trapList.Count);

            if (trapList[i] != null)
            {
                trapList[i].SetActive(true);
            }
        }
    }
}