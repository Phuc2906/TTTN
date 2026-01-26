using UnityEngine;
using System.Collections.Generic;

public class Gun_Enemy : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;

    [Header("Detect")]
    public float detectionRange = 8f;
    public List<Transform> players;   

    private Transform currentPlayer;
    private float fireTimer;

    private SpriteRenderer enemySprite;

    void Start()
    {
        enemySprite = GetComponentInParent<SpriteRenderer>();
        fireTimer = 0f;
    }

    void Update()
    {
        FindActivePlayer();
        if (currentPlayer == null) return;

        fireTimer -= Time.deltaTime;

        FlipWithEnemy();
        StickGunHorizontally();

        float distance = Vector2.Distance(firePoint.position, currentPlayer.position);
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


    void FindActivePlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null && players[i].gameObject.activeInHierarchy)
            {
                currentPlayer = players[i];
                return;
            }
        }

        currentPlayer = null;
    }

    void AimAtPlayer()
    {
        Vector2 direction = (currentPlayer.position - firePoint.position).normalized;
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

    void FlipWithEnemy()
    {
        if (enemySprite == null) return;

        Vector3 scale = transform.localScale;
        scale.x = enemySprite.flipX ? -1 : 1;
        transform.localScale = scale;
    }

    void StickGunHorizontally()
    {
        transform.localEulerAngles = Vector3.zero;
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
