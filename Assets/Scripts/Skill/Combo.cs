using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public List<GameObject> GameObject_01 = new List<GameObject>();
    public List<GameObject> GameObject_02 = new List<GameObject>();

    public float delay = 1f;

    private bool state = true;

    void Start()
    {
        ApplyState();
        StartCoroutine(ToggleOnce());
    }

    IEnumerator ToggleOnce()
    {
        yield return new WaitForSeconds(delay);

        state = !state;
        ApplyState();
    }

    void ApplyState()
    {
        foreach (GameObject obj in GameObject_01)
        {
            if (obj != null)
                obj.SetActive(state);
        }

        foreach (GameObject obj in GameObject_02)
        {
            if (obj != null)
                obj.SetActive(!state);
        }
    }
}