using UnityEngine;

public class ItemIconDatabase : MonoBehaviour
{
    public static ItemIconDatabase Instance;

    public string[] keys;
    public Sprite[] icons;

    void Awake()
    {
        Instance = this;
    }

    public Sprite GetIcon(string key)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i] == key)
                return icons[i];
        }
        return null;
    }
}
