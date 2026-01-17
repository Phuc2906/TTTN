using UnityEngine;

public class GunFlip_Enemy : MonoBehaviour
{
    public Transform enemy;  

    void LateUpdate()
    {
        if (enemy == null) return;

        if (enemy.localScale.x < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        transform.rotation = Quaternion.identity;
    }
}
