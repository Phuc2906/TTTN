using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public List<GameObject> GameObject_01 = new List<GameObject>();
    public List<GameObject> GameObject_02 = new List<GameObject>();

    public float delayToSecond = 1f;
    public float delayToOff = 1.75f;

    private Coroutine currentRoutine;

    void OnEnable()
    {
        SetList(GameObject_01, false);
        SetList(GameObject_02, false);

        currentRoutine = StartCoroutine(PlaySequence());
    }

    void OnDisable()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
    }

    IEnumerator PlaySequence()
    {
        SetList(GameObject_01, true);

        yield return new WaitForSeconds(delayToSecond);

        SetList(GameObject_01, false);
        SetList(GameObject_02, true);

        yield return new WaitForSeconds(delayToOff);

        SetList(GameObject_02, false);
    }

    void SetList(List<GameObject> list, bool value)
    {
        foreach (GameObject obj in list)
        {
            if (obj != null)
                obj.SetActive(value);
        }
    }
}