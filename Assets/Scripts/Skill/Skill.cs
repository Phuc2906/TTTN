using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject obj2;
    public float delay = 1f;

    void OnEnable()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);

        if (obj2 != null)
            obj2.SetActive(true);
    }
}