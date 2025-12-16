using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weaponHolder;

    [Header("Default Weapon")]
    public GameObject gunPrefab;

    private GameObject currentWeapon;
    private SpriteRenderer playerSprite;
    private Gun currentGun;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        EquipWeapon(gunPrefab);
    }

    void Update()
    {
        if (currentWeapon == null) return;

        SpriteRenderer weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        if (weaponSprite != null)
            weaponSprite.flipX = playerSprite.flipX;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - currentWeapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null) return;

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(
            weaponPrefab,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );

        currentGun = currentWeapon.GetComponent<Gun>();
    }
}
