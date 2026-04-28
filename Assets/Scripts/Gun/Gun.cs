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
    public LayerMask enemyBossLayer;

    [Header("Fire Mode")]
    public bool isAutoFire = true;
    public KeyCode manualKey = KeyCode.Z;

    [Header("Buff Range")]
    public GameObject rangeBuffCanvas;
    public float rangeBoost = 3f;

    [Header("Buff Fire Rate")]
    public GameObject fireRateBuffCanvas;
    public float fireRateBoost = 0.2f;

    private float fireTimer = 0f;
    private SpriteRenderer playerSprite;

    [Header("Shadow Lock")]
    public List<GameObject> shadows = new List<GameObject>();

    [Header("Current Bullet Damage")]
    public int currentBulletDamage = 1;

    void Start()
    {
        playerSprite = GetComponentInParent<SpriteRenderer>();
        fireTimer = 0f;

        if (bulletPrefab != null)
        {
            Bullet b = bulletPrefab.GetComponent<Bullet>();
            if (b != null)
                currentBulletDamage = b.normalDamage;
        }
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        FlipWithPlayer();
        StickGunHorizontally();

        Transform nearestEnemy = FindNearestEnemy();

        if (isAutoFire)
        {
            AutoMode(nearestEnemy);
        }
        else
        {
            ManualMode(nearestEnemy);
        }
    }
    void AutoMode(Transform nearestEnemy)
    {
        if (nearestEnemy == null)
            return;

        AimAtEnemy(nearestEnemy.position);

        if (fireTimer <= 0f && !IsAnyShadowActive())
        {
            Shoot();
            ResetFireRate();
        }
    }

    void ManualMode(Transform nearestEnemy)
    {
        if (Input.GetKey(manualKey))
        {
            if (fireTimer > 0f || IsAnyShadowActive())
                return;

            if (nearestEnemy != null)
                AimAtEnemy(nearestEnemy.position);
            else
                AimAtPlayerForward();

            Shoot();
            ResetFireRate(true);
        }
    }

    void ResetFireRate(bool isManual = false)
    {
        float finalFireRate = fireRate;

        if (fireRateBuffCanvas != null && fireRateBuffCanvas.activeSelf)
        {
            finalFireRate -= fireRateBoost;
            if (finalFireRate < 0.05f)
                finalFireRate = 0.05f;
        }

        if (isManual)
            finalFireRate *= 0.85f; 

        fireTimer = finalFireRate;
    }

    void AimAtPlayerForward()
    {
        if (playerSprite == null) return;

        Vector3 dir = playerSprite.flipX ? Vector3.left : Vector3.right;
        firePoint.right = dir;
    }

    void Shoot()
    {
        if (!bulletPrefab) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = firePoint.right * bulletSpeed;

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX(SoundManager.instance.shootSFX);
    }

    public void SetFireMode(bool autoMode)
    {
        isAutoFire = autoMode;
        fireTimer = 0f; 
    }

    Transform FindNearestEnemy()
    {
        float finalRange = detectionRange;

        if (rangeBuffCanvas != null && rangeBuffCanvas.activeSelf)
            finalRange += rangeBoost;

        Collider2D[] hits = Physics2D.OverlapCircleAll(firePoint.position, finalRange, enemyLayer | enemyBossLayer);

        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("EnemyBoss"))
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

    bool IsAnyShadowActive()
    {
        foreach (var s in shadows)
        {
            if (s != null && s.activeInHierarchy)
                return true;
        }
        return false;
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
        transform.localEulerAngles = Vector3.zero;
    }
}