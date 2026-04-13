using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Camera miniMapCamera;

    public float zoomStep = 2f;
    public float minSize = 5f;
    public float maxSize = 25f;

    public void ZoomIn()
    {
        miniMapCamera.orthographicSize += zoomStep;
        miniMapCamera.orthographicSize = Mathf.Clamp(miniMapCamera.orthographicSize, minSize, maxSize);
    }

    public void ZoomOut()
    {
        miniMapCamera.orthographicSize -= zoomStep;
        miniMapCamera.orthographicSize = Mathf.Clamp(miniMapCamera.orthographicSize, minSize, maxSize);
    }
}