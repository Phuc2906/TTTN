using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public GameObject buffCanvas;
    private Rigidbody2D rb;
    private Vector2 move;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.gravityScale = 0;

        if (PlayerPrefs.HasKey("PlayerX"))
        {
            transform.position = new Vector2(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY")
            );
        }
        if (PlayerPrefs.HasKey("PlayerFacing"))
        {
            sr.flipX = PlayerPrefs.GetInt("PlayerFacing") == 1;
        }
    }

    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move.Normalize();

        if (move.x > 0) sr.flipX = false;
        if (move.x < 0) sr.flipX = true;

        anim.SetBool("IsRunning", move.magnitude > 0);
    }

    void FixedUpdate()
    {
        float finalSpeed = speed;

        if (buffCanvas != null && buffCanvas.activeSelf)
        {
            finalSpeed += 2f;
        }

        rb.MovePosition(rb.position + move * finalSpeed * Time.fixedDeltaTime);
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);

        PlayerPrefs.SetInt("PlayerFacing", sr.flipX ? 1 : 0);

        PlayerPrefs.Save();
    }
}
