using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    [Header("Group ID")]
    public int groupID;

    [Header("Enemy IDs trong group này")]
    public int[] enemyIDs;

    [Header("Objects To Enable")]
    public GameObject[] objectsToEnable;

    [Header("Objects To Disable")]
    public GameObject[] objectsToDisable;

    private bool activated;

    void Start()
    {
        activated = PlayerPrefs.GetInt("EnemyGroup_" + groupID, 0) == 1;

        if (activated)
        {
            ApplyState();
        }
    }

    void Update()
    {
        if (activated) return;

        if (AllEnemiesDead())
        {
            activated = true;
            PlayerPrefs.SetInt("EnemyGroup_" + groupID, 1);
            PlayerPrefs.Save();
            ApplyState();
        }
    }

    bool AllEnemiesDead()
    {
        foreach (int id in enemyIDs)
        {
            if (PlayerPrefs.GetInt("Enemy_" + id, 0) == 0)
                return false;
        }
        return true;
    }

    void ApplyState()
    {
        foreach (GameObject obj in objectsToEnable)
            if (obj) obj.SetActive(true);

        foreach (GameObject obj in objectsToDisable)
            if (obj) obj.SetActive(false);
    }
}
