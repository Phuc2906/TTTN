using UnityEngine;
using System.Collections.Generic;

public class MiniCamera : MonoBehaviour
{
    public List<GameObject> players;
    public float height = -10f;

    void LateUpdate()
    {
        foreach (var p in players)
        {
            if (p != null && p.activeInHierarchy)
            {
                transform.position = new Vector3(
                    p.transform.position.x,
                    p.transform.position.y,
                    height
                );
                break;
            }
        }
    }
}