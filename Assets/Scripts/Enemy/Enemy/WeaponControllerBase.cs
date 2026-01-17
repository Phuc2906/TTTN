using UnityEngine;

public class WeaponControllerBase : MonoBehaviour
{
    [Header("Weapon")]
    public Transform weaponHolder;
    protected GameObject currentWeapon;
    protected SpriteRenderer ownerSprite;

    protected virtual void Awake()
    {
        ownerSprite = GetComponent<SpriteRenderer>();
    }

    protected void EquipWeapon(GameObject weaponPrefab)
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
    }

    protected void RotateWeapon(Vector3 targetPos)
    {
        if (currentWeapon == null) return;

        Vector3 dir = targetPos - currentWeapon.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);

        var sr = currentWeapon.GetComponent<SpriteRenderer>();
        if (sr != null && ownerSprite != null)
            sr.flipX = ownerSprite.flipX;
    }
}
