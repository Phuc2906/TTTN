using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public GameObject buffCanvas;

    [Header("Admin Speed Override")]
    public float adminSpeed = 0f;

    [Header("Text TMP")]
    public TMP_Text speedText;

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

        if (SoundManager.instance != null && SoundManager.instance.sfxSource != null)
        {
            moveAudio.volume = SoundManager.instance.sfxSource.volume;
        }
        else
        {
            moveAudio.volume = 1f;
        }

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

        if (PlayerPrefs.HasKey("AdminSpeed"))
    {
        adminSpeed = PlayerPrefs.GetFloat("AdminSpeed");
    }

        UpdateSpeedUI(); 
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

        UpdateSpeedUI(); 
    }

    void FixedUpdate()
    {
        float finalSpeed = GetFinalSpeed();

        rb.MovePosition(rb.position + move * finalSpeed * Time.fixedDeltaTime);
    }

    float GetFinalSpeed()
    {
        float finalSpeed = (adminSpeed > 0) ? adminSpeed : speed;

        if (buffCanvas != null && buffCanvas.activeSelf)
        {
            finalSpeed += 2f;
        }

        return finalSpeed;
    }

    public void SetSpeed(float value)
    {
        adminSpeed = value;

        PlayerPrefs.SetFloat("AdminSpeed", adminSpeed);
        PlayerPrefs.Save();
        
        UpdateSpeedUI();
    }

    void UpdateSpeedUI()
    {
        if (speedText == null) return;

        float finalSpeed = GetFinalSpeed();
        speedText.text = finalSpeed.ToString();
    }

    void HandleMoveSound()
    {
        if (SoundManager.instance == null || moveAudio == null) return;
        if (SoundManager.instance.sfxSource == null) return;

        bool isMoving = move.magnitude > 0;

        moveAudio.volume = SoundManager.instance.sfxSource.volume;

        if (isMoving && !SoundManager.instance.sfxSource.mute)
        {
            if (!moveAudio.isPlaying && SoundManager.instance.moveSFX != null)
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