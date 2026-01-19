using UnityEngine;

public class Close2CV : MonoBehaviour
{
    public GameObject Canvas_A;
    public GameObject Canvas_B;
    public GameObject Canvas_C;

    [Header("NPC")]
    public GameObject npc;
    public string npcDestroyedKey = "NPC_A_Destroyed";

    public void CanvasOnClick()
    {
        Canvas_A.SetActive(false);
        Canvas_B.SetActive(false);
        Canvas_C.SetActive(true);

        PlayerPrefs.SetInt(npcDestroyedKey, 1);
        PlayerPrefs.Save();

        Destroy(npc);
    }
}
