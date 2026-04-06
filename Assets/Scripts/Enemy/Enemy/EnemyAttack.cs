using UnityEngine;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 5;
    public float attackCooldown = 0.6f;
    public float attackRange = 1.25f;

    [Header("Players")]
    public List<PlayerHealth> players;

    [Header("Other Targets")]
    public List<TeammateHealth> teammates;
    public List<HealthRuby> rubies;
    public List<HealthWall> walls;

    [HideInInspector] public bool isAttacking;

    private float lastHitTime;

    void Update()
    {
        bool foundTarget = false;

        foreach (var p in players)
        {
            if (p == null || !p.gameObject.activeInHierarchy) continue;

            if (IsInRange(p.transform))
            {
                AttackPlayer(p);
                foundTarget = true;
                break;
            }
        }

        if (!foundTarget)
        {
            foreach (var t in teammates)
            {
                if (t == null || !t.gameObject.activeInHierarchy) continue;

                if (IsInRange(t.transform))
                {
                    AttackTeammate(t);
                    foundTarget = true;
                    break;
                }
            }
        }

        if (!foundTarget)
        {
            foreach (var r in rubies)
            {
                if (r == null) continue;

                if (IsInRange(r.transform))
                {
                    AttackRuby(r);
                    foundTarget = true;
                    break;
                }
            }
        }

        if (!foundTarget)
        {
            foreach (var w in walls)
            {
                if (w == null) continue;

                if (IsInRange(w.transform))
                {
                    AttackWall(w);
                    foundTarget = true;
                    break;
                }
            }
        }

        isAttacking = foundTarget;
    }

    bool IsInRange(Transform target)
    {
        Collider2D myCol = GetComponent<Collider2D>();
        Collider2D targetCol = target.GetComponent<Collider2D>();

        if (myCol == null || targetCol == null) return false;

        return myCol.Distance(targetCol).distance <= 0.05f;
    }

    void AttackPlayer(PlayerHealth p)
    {
        if (Time.time - lastHitTime < attackCooldown) return;

        p.TakeDamage(damage);
        lastHitTime = Time.time;
    }

    void AttackTeammate(TeammateHealth t)
    {
        if (Time.time - lastHitTime < attackCooldown) return;

        t.TakeDamage(damage);
        lastHitTime = Time.time;
    }

    void AttackRuby(HealthRuby r)
    {
        if (Time.time - lastHitTime < attackCooldown) return;

        r.TakeDamage(damage);
        lastHitTime = Time.time;
    }

    void AttackWall(HealthWall w)
    {
        if (Time.time - lastHitTime < attackCooldown) return;

        w.TakeDamage(damage);
        lastHitTime = Time.time;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}