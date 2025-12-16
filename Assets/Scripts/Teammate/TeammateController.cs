using UnityEngine;

public class TeammateController : MonoBehaviour
{
    public GameObject targetObject;           
    public TeammateMove teammateMove;          

    void Update()
    {
        if (targetObject == null) return;

        bool isActive = targetObject.activeSelf;

        if (isActive)
        {
            if (teammateMove.enabled) teammateMove.enabled = false;
        }
        else
        {
            if (!teammateMove.enabled) teammateMove.enabled = true;
        }
    }
}
