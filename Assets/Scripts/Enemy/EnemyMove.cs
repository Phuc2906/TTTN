using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour
{
    [Header("Target & Movement")]
    public Transform player;
    public float moveSpeed = 2.5f;
    public float repathRate = 0.25f;

    [Header("Fallback Movement")]
    public float directMoveSpeed = 2f;

    private EnemyAttack attack;
    private SpriteRenderer sr;

    private AStarPathfinding pathfinder;
    private List<Node> path;
    private int currentIndex = 0;

    void Start()
    {
        attack = GetComponent<EnemyAttack>();
        sr = GetComponent<SpriteRenderer>();

        pathfinder = FindObjectOfType<AStarPathfinding>();
        StartCoroutine(UpdatePathRoutine());
    }

    IEnumerator UpdatePathRoutine()
    {
        while (true)
        {
            if (pathfinder != null && player != null)
            {
                path = pathfinder.FindPath(transform.position, player.position);
                currentIndex = 0;
            }
            yield return new WaitForSeconds(repathRate);
        }
    }

    void Update()
    {
        if (attack != null && attack.isAttacking)
            return;

        if (!player)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (!player) return;

        if (path != null && path.Count > 0)
        {
            Vector3 targetPos = path[currentIndex].worldPos;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                currentIndex++;
                if (currentIndex >= path.Count)
                {
                    path = null; 
                }
            }

            Vector3 moveDir = (targetPos - transform.position).normalized;
            if (moveDir.x > 0.05f) sr.flipX = false;
            else if (moveDir.x < -0.05f) sr.flipX = true;
        }
        else 
        {
            Vector2 pos = transform.position;
            Vector2 dir = ((Vector2)player.position - pos).normalized;
            transform.position = pos + dir * directMoveSpeed * Time.deltaTime;

            if (dir.x > 0.05f) sr.flipX = false;
            else if (dir.x < -0.05f) sr.flipX = true;
        }
    }
}
