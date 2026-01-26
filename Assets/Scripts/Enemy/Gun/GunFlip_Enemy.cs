using UnityEngine;

public class GunFlip_Enemy : MonoBehaviour
{
    public Transform enemy;  

    void LateUpdate()
    {
        if (enemy == null) return;

        if (enemy.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        transform.rotation = Quaternion.identity;
    }
}
