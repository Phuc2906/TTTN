using UnityEngine;

public class OpenGameObject : MonoBehaviour
{
    [SerializeField] private GameObject targetObject_A; 
    [SerializeField] private GameObject targetObject_B; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetObject_A != null)
                targetObject_A.SetActive(true);

            StartCoroutine(DelayActivateB());
        }
    }

    private System.Collections.IEnumerator DelayActivateB()
    {
        yield return new WaitForSeconds(0.5f); 
        if (targetObject_B != null)
            targetObject_B.SetActive(true);
    }
}
