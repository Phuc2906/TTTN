using UnityEngine;

public class Weapon : WeaponControllerBase
{
    public Transform player;
    public GameObject gunPrefab;

    void Start()
    {
        EquipWeapon(gunPrefab);
    }

    void Update()
    {
        if (player == null) return;
        RotateWeapon(player.position);
    }
}
