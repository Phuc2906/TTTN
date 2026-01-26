using UnityEngine;
using System.Collections.Generic;

public class TeammateMove : MonoBehaviour
{
    [Header("Save")]
    public int teammateID;

    [Header("Target")]
    public List<Transform> players = new List<Transform>();
    private Transform player;

    [Header("Settings")]
    public float moveSpeed = 2f;
    public float stopDistance = 1.5f;

    private EnemyAttack attack;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    string keyX;
    string keyY;
    string keyFacing;

    bool allowSave = false; 

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attack = GetComponent<EnemyAttack>();

        keyX = "Teammate_X_" + teammateID;
        keyY = "Teammate_Y_" + teammateID;
        keyFacing = "Teammate_Facing_" + teammateID;

        if (PlayerPrefs.HasKey(keyX) && PlayerPrefs.HasKey(keyY))
        {
            float x = PlayerPrefs.GetFloat(keyX);
            float y = PlayerPrefs.GetFloat(keyY);
            transform.position = new Vector3(x, y, transform.position.z);
        }

        if (sr && PlayerPrefs.HasKey(keyFacing))
            sr.flipX = PlayerPrefs.GetInt(keyFacing) == 1;
    }

    void Start()
    {
        allowSave = true; 
    }

    void Update()
    {
        FindActivePlayer();
        if ((attack != null && attack.isAttacking) || player == null)
        {
            SetRunning(false);
            return;
        }

        MoveTowardsPlayer();

        if (!allowSave) return;

        PlayerPrefs.SetFloat(keyX, transform.position.x);
        PlayerPrefs.SetFloat(keyY, transform.position.y);

        if (sr)
            PlayerPrefs.SetInt(keyFacing, sr.flipX ? 1 : 0);
    }

    void FindActivePlayer()
    {
        if (player != null && player.gameObject.activeInHierarchy)
            return;

        player = null;

        foreach (var p in players)
        {
            if (p != null && p.gameObject.activeInHierarchy)
            {
                player = p;
                return;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector2.Distance(rb.position, player.position);
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;

        if (distance > stopDistance)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            SetRunning(true);

            if (sr)
                sr.flipX = direction.x < 0;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            SetRunning(false);
        }
    }

    void SetRunning(bool state)
    {
        if (anim)
            anim.SetBool("IsRunning", state);
    }

    public void DeleteSave()
    {
        allowSave = false;
        PlayerPrefs.DeleteKey(keyX);
        PlayerPrefs.DeleteKey(keyY);
        PlayerPrefs.DeleteKey(keyFacing);
    }
}
