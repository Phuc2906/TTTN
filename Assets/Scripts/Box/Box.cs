using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    public GameObject targetCanvas;
    public Animator animator;

    [Header("Box ID (phải khác nhau)")]
    public int boxID = 0;

    private bool triggered = false;
    private Collider2D boxCollider;

    private string BOX_KEY;

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
        BOX_KEY = "Box_" + boxID;

        if (PlayerPrefs.GetInt(BOX_KEY, 0) == 1)
        {
            triggered = true;

            if (boxCollider != null)
                boxCollider.enabled = false;

            if (animator != null)
            {
                animator.Play("Box_01", 0, 1f);
                animator.Update(0f);
            }

            if (targetCanvas != null)
                targetCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || triggered) return;

        triggered = true;

        if (boxCollider != null)
            boxCollider.enabled = false;

        animator.SetTrigger("Open");

        PlayerPrefs.SetInt(BOX_KEY, 1);
        PlayerPrefs.Save();

        StartCoroutine(OpenCanvasAfterAnim());
    }

    IEnumerator OpenCanvasAfterAnim()
    {
        yield return null;

        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
        float animLength = clips[0].clip.length;

        yield return new WaitForSeconds(animLength);

        if (targetCanvas != null)
            targetCanvas.SetActive(true);

        Time.timeScale = 0f;
    }
}
