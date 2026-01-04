using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
    enum AIState { Idle, Chase, WallFollow }

    [Header("Save")]
    public int enemyID;

    [Header("Target")]
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float detectRange = 8f;
    public float stopDistance = 1.1f;

    [Header("Obstacle")]
    public float wallCheckDistance = 1f;
    public LayerMask obstacleMask;

    [Header("Wall Follow Fix")]
    public float wallPushForce = 0.2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EnemyAttack attack;

    private AIState state;
    private Vector2 wallDir;
    private Vector2 lastWallNormal;

    string keyX;
    string keyY;
    string keyFacing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        attack = GetComponent<EnemyAttack>();

        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        keyX = "Enemy_X_" + enemyID;
        keyY = "Enemy_Y_" + enemyID;
        keyFacing = "Enemy_Facing_" + enemyID;

        if (PlayerPrefs.HasKey(keyX) && PlayerPrefs.HasKey(keyY))
        {
            float x = PlayerPrefs.GetFloat(keyX);
            float y = PlayerPrefs.GetFloat(keyY);
            transform.position = new Vector3(x, y, transform.position.z);
        }

        if (PlayerPrefs.HasKey(keyFacing) && sr)
        {
            sr.flipX = PlayerPrefs.GetInt(keyFacing) == 1;
        }
    }

    void FixedUpdate()
    {
        FindPlayerIfNeeded();
        if (!player) return;

        if (attack && attack.isAttacking) return;

        UpdateState();
        Move();

        PlayerPrefs.SetFloat(keyX, transform.position.x);
        PlayerPrefs.SetFloat(keyY, transform.position.y);

        if (sr)
            PlayerPrefs.SetInt(keyFacing, sr.flipX ? 1 : 0);
    }

    void UpdateState()
    {
        float dist = Vector2.Distance(rb.position, player.position);

        if (dist > detectRange)
        {
            state = AIState.Idle;
            return;
        }

        if (state == AIState.WallFollow)
        {
            if (HasClearPathToPlayer())
                state = AIState.Chase;
            return;
        }

        if (CheckWallAhead(out RaycastHit2D hit))
        {
            EnterWallFollow(hit);
            return;
        }

        state = AIState.Chase;
    }

    void Move()
    {
        Vector2 nextPos = rb.position;

        if (state == AIState.Chase)
        {
            Vector2 dir = ((Vector2)player.position - rb.position).normalized;

            if (Vector2.Distance(rb.position, player.position) > stopDistance)
                nextPos += dir * moveSpeed * Time.fixedDeltaTime;

            Flip(dir.x);
        }
        else if (state == AIState.WallFollow)
        {
            Vector2 pushAway = lastWallNormal * wallPushForce;
            nextPos += (wallDir + pushAway) * moveSpeed * Time.fixedDeltaTime;

            Flip(wallDir.x);
        }

        rb.MovePosition(nextPos);
    }

    bool CheckWallAhead(out RaycastHit2D hit)
    {
        Vector2 dirToPlayer = ((Vector2)player.position - rb.position).normalized;
        hit = Physics2D.Raycast(rb.position, dirToPlayer, wallCheckDistance, obstacleMask);
        return hit.collider != null;
    }

    void EnterWallFollow(RaycastHit2D hit)
    {
        lastWallNormal = hit.normal;

        Vector2 tangent = Vector2.Perpendicular(lastWallNormal);
        Vector2 toPlayer = ((Vector2)player.position - rb.position).normalized;

        wallDir = Vector2.Dot(tangent, toPlayer) > 0 ? tangent : -tangent;
        wallDir.Normalize();

        state = AIState.WallFollow;
    }

    bool HasClearPathToPlayer()
    {
        Vector2 dir = ((Vector2)player.position - rb.position).normalized;
        float dist = Vector2.Distance(rb.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(rb.position, dir, dist, obstacleMask);
        return hit.collider == null;
    }

    void Flip(float x)
    {
        if (!sr) return;
        if (Mathf.Abs(x) < 0.05f) return;
        sr.flipX = x < 0;
    }

    void FindPlayerIfNeeded()
    {
        if (player) return;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
    }
}
