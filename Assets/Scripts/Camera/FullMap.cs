using UnityEngine;

public class FullMap : MonoBehaviour
{
    public Camera mapCamera;

    [Header("Zoom")]
    public float zoomStep = 5f;
    public float minSize = 5f;
    public float maxSize = 50f;

    [Header("Pan")]
    public float panLimit = 50f;

    private Vector3 dragOrigin;

    void Update()
    {
        HandleDrag();
    }

    public void ZoomIn()
    {
        mapCamera.orthographicSize += zoomStep;
        mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize, minSize, maxSize);
    }

    public void ZoomOut()
    {
        mapCamera.orthographicSize -= zoomStep;
        mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize, minSize, maxSize);
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = mapCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - mapCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = mapCamera.transform.position + difference;

            newPos.x = Mathf.Clamp(newPos.x, -panLimit, panLimit);
            newPos.y = Mathf.Clamp(newPos.y, -panLimit, panLimit);

            mapCamera.transform.position = newPos;
        }
    }
}