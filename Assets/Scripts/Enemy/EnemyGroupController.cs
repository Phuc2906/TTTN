using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    [Header("Objects To Enable")]
    public GameObject[] objectsToEnable;

    [Header("Objects To Disable")]
    public GameObject[] objectsToDisable;

    private bool activated = false;

    void Update()
    {
        if (activated) return;

        if (transform.childCount == 0)
        {
            activated = true;

            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                    obj.SetActive(true);
            }

            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
        }
    }
}
