using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    public float detectionRange = 8f;
    public LayerMask enemyLayer; 

    private float fireTimer = 0f;
    private SpriteRenderer playerSprite;

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

            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate;
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(firePoint.position, detectionRange, enemyLayer);
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
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.linearVelocity = firePoint.right * bulletSpeed;
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
