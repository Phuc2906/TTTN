using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    public GameObject buffCanvas;   
    public int normalDamage = 1;    

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Enemy"))
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            int finalDamage = normalDamage;
            if (buffCanvas != null && buffCanvas.activeSelf)
                finalDamage *= 2;
            enemy.TakeDamage(finalDamage);
            Destroy(gameObject);
            return;  
        }
    }
}
}
