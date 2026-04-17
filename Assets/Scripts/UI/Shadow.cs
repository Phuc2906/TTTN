using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shadow : MonoBehaviour
{
    [Header("Enemies")]
    public List<EnemyMove> enemyMoves = new List<EnemyMove>();
    public List<EnemyAttack> enemyAttacks = new List<EnemyAttack>();

    [Header("Shadow Effect")]
    public GameObject shadowObject;
    public float delayTime = 1.5f;

    private bool hasStarted = false;

    void OnEnable()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            return;
        }

        StartCoroutine(ShadowRoutine());
    }

    IEnumerator ShadowRoutine()
    {
        if (shadowObject != null)
            shadowObject.SetActive(true);

        foreach (var e in enemyMoves)
        {
            if (e != null) e.enabled = false;
        }

        foreach (var a in enemyAttacks)
        {
            if (a != null) a.enabled = false;
        }

        yield return new WaitForSeconds(delayTime);

        if (shadowObject != null)
            shadowObject.SetActive(false);

        foreach (var e in enemyMoves)
        {
            if (e != null) e.enabled = true;
        }

        foreach (var a in enemyAttacks)
        {
            if (a != null) a.enabled = true;
        }
    }

    void OnDisable()
    {
        foreach (var e in enemyMoves)
        {
            if (e != null) e.enabled = true;
        }

        foreach (var a in enemyAttacks)
        {
            if (a != null) a.enabled = true;
        }
    }
}