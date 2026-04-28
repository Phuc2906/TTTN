using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class TrapManager : MonoBehaviour
{
    public List<GameObject> targetObjs; 

    [Header("Thời gian mỗi trạng thái (giây)")]
    public float interval = 2f;

    public bool autoStart = true;

    void Start()
    {
        if (autoStart) StartToggleLoop();
    }

    public void StartToggleLoop()
    {
        StartCoroutine(ToggleLoop());
    }

    IEnumerator ToggleLoop()
    {
        while (true)
        {
            foreach (GameObject obj in targetObjs)
            {
                if (obj != null) obj.SetActive(true);
            }

            yield return new WaitForSeconds(interval);

            foreach (GameObject obj in targetObjs)
            {
                if (obj != null) obj.SetActive(false);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}