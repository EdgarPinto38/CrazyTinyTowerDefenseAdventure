using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private float damage;
    private ObjectPool objectPool;

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Infligir daño al enemigo
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Projectile hit enemy: " + other.name + " for " + damage + " damage.");
            }
            ReturnToPool();
        }
    }

    private void OnBecameInvisible()
    {
        // Devolver el proyectil al pool cuando salga de la pantalla
        ReturnToPool();
    }

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
