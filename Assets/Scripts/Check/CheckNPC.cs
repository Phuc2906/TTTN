using UnityEngine;

public class CheckNPC : MonoBehaviour
{
    [Header("ID NPC")]
    public string npcID = "NPC_Answer_02";

    void Start()
    {
        if (PlayerPrefs.GetInt(npcID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyNPC()
    {
        PlayerPrefs.SetInt(npcID, 1);
        PlayerPrefs.Save();

        Destroy(gameObject);
    }
}