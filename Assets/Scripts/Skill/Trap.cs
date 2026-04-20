using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Damage over time")]
    public int damage = 25;
    public float damageInterval = 1f;

    private float timer;

    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        if (timer < damageInterval) return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
            timer = 0f;
            return;
        }

        TeammateHealth teammate = other.GetComponent<TeammateHealth>();
        if (teammate != null)
        {
            teammate.TakeDamage(damage);
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null ||
            other.GetComponent<TeammateHealth>() != null)
        {
            timer = 0f;
        }
    }
}