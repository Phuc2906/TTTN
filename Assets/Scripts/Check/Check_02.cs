using UnityEngine;

public class Check_02 : MonoBehaviour
{
    [Header("Save")]
    public string rentID = "Rent_NPC_01";

    void Start()
    {
        if (PlayerPrefs.GetInt(rentID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }
}
