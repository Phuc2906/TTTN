using System.Collections.Generic;
using UnityEngine;

public class AdminTeleport : MonoBehaviour
{
    [Header("Player List")]
    public List<GameObject> players = new List<GameObject>();

    [Header("Teleport Position")]
    public float posX;
    public float posY;

    private GameObject currentPlayer;

    void Update()
    {
        currentPlayer = null;

        foreach (GameObject player in players)
        {
            if (player != null && player.activeInHierarchy)
            {
                currentPlayer = player;
                break;
            }
        }
    }

    public void Teleport()
    {
        if (currentPlayer != null)
        {
            currentPlayer.transform.position = new Vector2(posX, posY);
        }
    }
}