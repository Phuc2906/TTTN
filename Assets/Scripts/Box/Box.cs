using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    public GameObject targetCanvas;
    public Animator animator;

    private bool triggered = false;
    private Collider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || triggered) return;

        triggered = true;

        if (boxCollider != null)
            boxCollider.enabled = false;

        animator.SetTrigger("Open");
        StartCoroutine(OpenCanvasAfterAnim());
    }

    IEnumerator OpenCanvasAfterAnim()
    {
        yield return null;

        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
        float animLength = clips[0].clip.length;

        yield return new WaitForSeconds(animLength);
        targetCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
