using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    public GameObject targetCanvas;
    public Animator animator;
    public string openAnimStateName = "Box_01"; 
    
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            triggered = true;

            targetCanvas.SetActive(true);
            animator.SetTrigger("Open");

            StartCoroutine(StopAfterFinish());
        }
    }

    IEnumerator StopAfterFinish()
    {
        yield return null;

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        while(!info.IsName(openAnimStateName))
        {
            yield return null;
            info = animator.GetCurrentAnimatorStateInfo(0);
        }

        while(info.normalizedTime < 1f)
        {
            yield return null;
            info = animator.GetCurrentAnimatorStateInfo(0);
        }

        animator.Play(openAnimStateName, 0, 1f); 
        animator.speed = 0;                      
    }
}
