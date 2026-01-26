using UnityEngine;

public class TeammateController : MonoBehaviour
{
    public GameObject targetObject;
    public TeammateMove teammateMove;

    void Awake()
    {
        ApplyState();
    }

    void OnEnable()
    {
        ApplyState();
    }

    void Update()
    {
        ApplyState();
    }

    void ApplyState()
    {
        if (teammateMove == null) return;

        bool shouldDisableMove = targetObject != null;

        if (teammateMove.enabled == shouldDisableMove)
        {
            teammateMove.enabled = !shouldDisableMove;
        }
    }
}
