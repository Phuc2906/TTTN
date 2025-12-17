using UnityEngine;

public class GunFlip_Enemy : MonoBehaviour
{
    public Transform enemy;  

    void LateUpdate()
    {
        if (enemy == null) return;

        if (enemy.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        transform.rotation = Quaternion.identity;
    }
}
