using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Check npc;
    public GameObject canvasToClose;

    public void OnClickDestroyNPC()
    {
        if (npc != null)
            npc.DestroyNPC();

        if (canvasToClose != null)
            canvasToClose.SetActive(false);
    }
}
