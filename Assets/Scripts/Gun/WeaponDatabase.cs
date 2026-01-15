using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public static WeaponDatabase Instance;

    public WeaponData[] weapons;

    void Awake()
    {
        Instance = this;
    }

    public WeaponData GetWeaponByKey(string key)
    {
        foreach (var w in weapons)
        {
            if (w.weaponKey == key)
                return w;
        }
        return null;
    }
}
