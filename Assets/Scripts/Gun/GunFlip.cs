using UnityEngine;

public class GunFlip : MonoBehaviour
{
    public Transform player;  

    void Update()
    {
        if (player == null || this == null || transform == null) 
            return;

        if (player.localScale.x < 0)
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        transform.rotation = Quaternion.identity;
    }
}