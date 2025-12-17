using UnityEngine;

public class Gun_Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public float detectionRange = 8f;
    public Transform player; 
    private float fireTimer = 0f;

    private SpriteRenderer enemySprite;

    void Start()
    {
        enemySprite = GetComponentInParent<SpriteRenderer>();
        fireTimer = 0f;
    }

    void Update()
    {
        if (player == null) return;

        fireTimer -= Time.deltaTime;

        FlipWithEnemy();        
        StickGunHorizontally(); 

        float distance = Vector2.Distance(firePoint.position, player.position);
        if (distance <= detectionRange)
        {
            AimAtPlayer();
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate;
            }
        }
    }
    void FlipWithEnemy()
    {
        if (enemySprite != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = enemySprite.flipX ? -1 : 1;
            transform.localScale = scale;
        }
    }
    void StickGunHorizontally()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    void AimAtPlayer()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
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
