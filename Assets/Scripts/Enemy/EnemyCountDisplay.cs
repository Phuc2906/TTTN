using UnityEngine;
using TMPro;

public class EnemyCountDisplay : MonoBehaviour
{
    public TMP_Text enemyCountText;
    public GameObject nationalAssemblyObject; 
    public Transform player;                
    public int targetCount = 200;
    public float spawnOffsetRadius = 2f;      

    private bool hasMoved = false; 

    void Start()
    {
        if (enemyCountText != null)
            enemyCountText.text = "0/" + targetCount;

        if (nationalAssemblyObject != null)
            nationalAssemblyObject.SetActive(false);
    }

    public void UpdateCount(int killed, int total)
    {
        if (enemyCountText != null)
            enemyCountText.text = killed + "/" + total;

        if (!hasMoved && killed >= targetCount)
        {
            ShowNearPlayer();
            hasMoved = true;
        }
    }

    void ShowNearPlayer()
    {
        if (player == null || nationalAssemblyObject == null)
            return;

        Vector2 randomOffset = Random.insideUnitCircle.normalized * spawnOffsetRadius;
        Vector3 newPos = player.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        nationalAssemblyObject.transform.position = newPos;
        nationalAssemblyObject.SetActive(true);
    }
}
