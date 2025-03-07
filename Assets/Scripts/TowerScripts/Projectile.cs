using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage;
    private ObjectPool objectPool;
    public float detectionRadius = 0.5f; // Radio de detección para las colisiones

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    private void Update()
    {
        // Mover el proyectil hacia la derecha en línea recta
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Detectar colisiones manualmente
        DetectCollisions();
    }

    private void DetectCollisions()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // Infligir daño al enemigo
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log("Projectile hit enemy: " + hit.name + " for " + damage + " damage.");
                    ReturnToPool();
                    break; // Salir del bucle después de detectar una colisión
                }
            }
        }
    }

    private void OnBecameInvisible()
    {
        // Devolver el proyectil al pool cuando salga de la pantalla
        ReturnToPool();
    }
    // Activar metodo Pool
    private void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.ReturnObject(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
