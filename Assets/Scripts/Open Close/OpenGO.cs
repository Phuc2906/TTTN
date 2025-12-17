using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenGO : MonoBehaviour
{
    [Header("Danh sách GameObject")]
    public List<GameObject> doors;

    [Header("Chỉ số bắt đầu delay ")]
    public int delayStartIndex = 1; 
    public float delayTime = 0.5f;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
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
        }
    }

    private IEnumerator ActivateWithDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
            obj.SetActive(true);
    }
}
