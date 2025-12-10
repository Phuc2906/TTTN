using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour
{
    public GameObject targetObj;

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
        while (true)           // lặp vô hạn
        {
            targetObj.SetActive(true);     // bật
            yield return new WaitForSeconds(interval);

            targetObj.SetActive(false);    // tắt
            yield return new WaitForSeconds(interval);
        }
    }
}
