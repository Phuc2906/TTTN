using UnityEngine;

public class GunFlip : MonoBehaviour
{
    public Transform player;  

    void Update()
    {
        if (player == null || this == null || transform == null) 
            return;

        if (player.localScale.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);

        transform.rotation = Quaternion.identity;
    }
}