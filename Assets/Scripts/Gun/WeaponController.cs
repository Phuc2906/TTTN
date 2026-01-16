using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weaponHolder;

    [Header("Default Weapon (Optional)")]
    public GameObject defaultWeaponPrefab;
    public string defaultWeaponKey;

    private GameObject currentWeapon;
    private SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentWeapon == null) return;

        SpriteRenderer weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        if (weaponSprite != null)
            weaponSprite.flipX = playerSprite.flipX;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)mousePos - (Vector2)currentWeapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void EquipWeaponByKey(string weaponKey)
    {
        if (string.IsNullOrEmpty(weaponKey))
        {
            UnequipWeapon();
            return;
        }

        WeaponData data = WeaponDatabase.Instance.GetWeaponByKey(weaponKey);

        if (data == null || data.weaponPrefab == null)
        {
            UnequipWeapon();
            return;
        }

        EquipWeapon(data.weaponPrefab);
    }

    void EquipWeapon(GameObject prefab)
    {
        if (prefab == null) return;

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(
            prefab,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = null;
    }
}
