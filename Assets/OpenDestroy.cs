using UnityEngine;
using System.Collections.Generic;

public class OpenDestroy : MonoBehaviour
{
    public Check npc;

    [Header("Canvases to Open")]
    public List<GameObject> canvasesToOpen = new List<GameObject>();
    [Header("Canvases to Close")]
    public List<GameObject> canvasesToClose = new List<GameObject>();

    public void OnClickOpenDestroyNPC()
    {
        if (npc != null)
            npc.DestroyNPC();

        foreach (var canvas in canvasesToOpen)
        {
            if (canvas != null && !canvas.activeSelf)
                canvas.SetActive(true);
        }
        foreach (var canvas in canvasesToClose)
        {
            if (canvas != null && canvas.activeSelf)
                canvas.SetActive(false);
        }
    }
}
