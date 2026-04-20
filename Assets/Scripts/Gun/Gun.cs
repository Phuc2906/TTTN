using UnityEngine;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    public float detectionRange = 8f;
    public LayerMask enemyLayer; 

    [Header("Buff Range")]
    public GameObject rangeBuffCanvas;
    public float rangeBoost = 3f;

    [Header("Buff Damage")]
    public GameObject damageBuffCanvas;

    [Header("Buff Fire Rate")]
    public GameObject fireRateBuffCanvas;
    public float fireRateBoost = 0.2f; 
    private float fireTimer = 0f;
    private SpriteRenderer playerSprite;

    [Header("Shadow Lock")]
    public List<GameObject> shadows = new List<GameObject>();

    void Start()
    {
        playerSprite = GetComponentInParent<SpriteRenderer>();
        fireTimer = 0f;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        FlipWithPlayer();        
        StickGunHorizontally();   

        Transform nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            AimAtEnemy(nearestEnemy.position);

           if (fireTimer <= 0f && !IsAnyShadowActive())
            {
                Shoot();

                float finalFireRate = fireRate;

            if (fireRateBuffCanvas != null && fireRateBuffCanvas.activeSelf)
            {
                finalFireRate -= fireRateBoost;

                if (finalFireRate < 0.05f)
                finalFireRate = 0.05f;
            }

                fireTimer = finalFireRate;
            }
        }
    }

    void FlipWithPlayer()
    {
        if (playerSprite != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = playerSprite.flipX ? -1 : 1;
            transform.localScale = scale;
        }
    }

    void StickGunHorizontally()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    Transform FindNearestEnemy()
    {
        float finalRange = detectionRange;

        if (rangeBuffCanvas != null && rangeBuffCanvas.activeSelf)
        {
            finalRange += rangeBoost;
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(firePoint.position, finalRange, enemyLayer);
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(firePoint.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = hit.transform;
                }
            }
        }
        return nearest;
    }

    void AimAtEnemy(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        if (!bulletPrefab) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            int finalDamage = bulletScript.normalDamage;

            if (damageBuffCanvas != null && damageBuffCanvas.activeSelf)
            {
                finalDamage *= 2;
            }

            bulletScript.SetDamage(finalDamage);
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * bulletSpeed;
        }

          if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.shootSFX);
        }
    }

    bool IsAnyShadowActive()
    {
        foreach (var s in shadows)
        {
            if (s != null && s.activeInHierarchy)
                return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position, detectionRange);
        }
    }
}