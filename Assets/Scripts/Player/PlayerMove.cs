using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public GameObject buffCanvas;

    private Rigidbody2D rb;
    private Vector2 move;
    private Animator anim;
    private SpriteRenderer sr;

    private AudioSource moveAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.gravityScale = 0;

        moveAudio = gameObject.AddComponent<AudioSource>();
        moveAudio.loop = true;
        moveAudio.playOnAwake = false;
        moveAudio.volume = SoundManager.instance.sfxSource.volume;

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

        HandleMoveSound();
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

    void HandleMoveSound()
    {
        if (SoundManager.instance == null) return;

        bool isMoving = move.magnitude > 0;

        moveAudio.volume = SoundManager.instance.sfxSource.volume;

        if (isMoving && !SoundManager.instance.sfxSource.mute)
        {
            if (!moveAudio.isPlaying)
            {
                moveAudio.clip = SoundManager.instance.moveSFX;
                moveAudio.pitch = Random.Range(0.9f, 1.1f);
                moveAudio.Play();
            }
        }
        else
        {
            if (moveAudio.isPlaying)
            {
                moveAudio.Stop();
            }
        }
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);

        PlayerPrefs.SetInt("PlayerFacing", sr.flipX ? 1 : 0);

        PlayerPrefs.Save();
    }
}