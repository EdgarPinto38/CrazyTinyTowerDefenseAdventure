using UnityEngine;

public class Tower : MonoBehaviour
{
    public float health = 100f;
    public float attackPower = 10f;
    public float attackRange = 5f;
    public float attackRate = 1f; // Disparos por segundo
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint; // Punto desde el cual se dispara el proyectil
    public ObjectPool projectilePool; // Referencia al Object Pool
    public LayerMask enemyLayer; // Capa de los enemigos

    private float attackCooldown = 0f;
    private float detectionCooldown = 0.1f; // Frecuencia de detección

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        detectionCooldown -= Time.deltaTime;

        if (detectionCooldown <= 0f)
        {
            DetectEnemy();
            detectionCooldown = 0.1f; // Reiniciar el tiempo de detección
        }

        if (attackCooldown <= 0f && IsEnemyInRange())
        {
            Attack();
            attackCooldown = 1f / attackRate;
        }
    }

    private void DetectEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, Vector2.right, attackRange, enemyLayer);
        if (hit.collider != null)
        {
            Debug.Log("Enemy detected: " + hit.collider.name);
        }
    }

    private bool IsEnemyInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, Vector2.right, attackRange, enemyLayer);
        return hit.collider != null;
    }

    protected virtual void Attack()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            // Obtener un proyectil del pool
            GameObject projectile = projectilePool.GetObject();
            projectile.transform.position = shootPoint.position;
            Projectile projScript = projectile.GetComponent<Projectile>();
            projScript.SetDamage(attackPower);
            projScript.SetObjectPool(projectilePool);
            Debug.Log("Tower attacks with power: " + attackPower);
        }
        else
        {
            Debug.LogError("projectilePrefab or shootPoint is not assigned.");
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Implementación general de la muerte de la torre
        Debug.Log("Tower has died.");
        Destroy(gameObject);
    }
}
