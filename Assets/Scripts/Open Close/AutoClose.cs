using UnityEngine;

public class AutoClose : MonoBehaviour
{
    public GameObject noticeCanvas;
    public float delay = 2f;  

    void OnEnable()
    {
        if (noticeCanvas == null)
            noticeCanvas = gameObject;

        StartCoroutine(CloseAfterDelay());
    }

    private System.Collections.IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        noticeCanvas.SetActive(false);
    }
}
