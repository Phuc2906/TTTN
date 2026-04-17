using UnityEngine;
using System.Collections;

public class EnemyGroupController : MonoBehaviour
{
    [Header("Group ID")]
    public int groupID;

    [Header("Enemy IDs trong group này")]
    public int[] enemyIDs;

    [Header("Objects To Enable")]
    public GameObject[] objectsToEnable;
    public bool useDelayEnable;
    public float enableDelay = 0.5f;

    [Header("Objects To Disable")]
    public GameObject[] objectsToDisable;
    public bool useDelayDisable;
    public float disableDelay = 0.5f;

    private bool activated;

    void Start()
    {
        activated = PlayerPrefs.GetInt("EnemyGroup_" + groupID, 0) == 1;
        ApplyStateInstant();
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
        if (activated)
        {
            if (useDelayEnable)
                StartCoroutine(EnableWithDelay());
            else
            {
                foreach (GameObject obj in objectsToEnable)
                    if (obj) obj.SetActive(true);
            }

            if (useDelayDisable)
                StartCoroutine(DisableWithDelay());
            else
            {
                foreach (GameObject obj in objectsToDisable)
                    if (obj) obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in objectsToEnable)
                if (obj) obj.SetActive(false);

            foreach (GameObject obj in objectsToDisable)
                if (obj) obj.SetActive(true);
        }
    }

    void ApplyStateInstant()
    {
        if (activated)
        {
            foreach (GameObject obj in objectsToEnable)
                if (obj) obj.SetActive(true);

            foreach (GameObject obj in objectsToDisable)
                if (obj) obj.SetActive(false);
        }
        else
        {
            foreach (GameObject obj in objectsToEnable)
                if (obj) obj.SetActive(false);

            foreach (GameObject obj in objectsToDisable)
                if (obj) obj.SetActive(true);
        }
    }

    IEnumerator EnableWithDelay()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            if (obj)
            {
                obj.SetActive(true);
                yield return new WaitForSeconds(enableDelay);
            }
        }
    }

    IEnumerator DisableWithDelay()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj)
            {
                obj.SetActive(false);
                yield return new WaitForSeconds(disableDelay);
            }
        }
    }
}