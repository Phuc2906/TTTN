using UnityEngine;

public class Check : MonoBehaviour
{
    public string npcID; 

    private void Start()
    {
        if (PlayerPrefs.GetInt(npcID + "_Destroyed", 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyNPC()
    {
        PlayerPrefs.SetInt(npcID + "_Destroyed", 1);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
}
