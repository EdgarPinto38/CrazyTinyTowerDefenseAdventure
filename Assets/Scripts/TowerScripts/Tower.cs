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
    private Enemy targetEnemy; // Referencia al enemigo objetivo

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        detectionCooldown -= Time.deltaTime;

        if (detectionCooldown <= 0f)
        {
            DetectEnemy();
            detectionCooldown = 0.1f; // Reiniciar el tiempo de detección
        }

        if (attackCooldown <= 0f && targetEnemy != null && IsEnemyInRange(targetEnemy))
        {
            Attack();
            attackCooldown = 1f / attackRate;
        }
        else
        {
            targetEnemy = null; // Si no hay enemigos en rango, dejar de atacar
        }
    }

    private void DetectEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, Vector2.right, attackRange, enemyLayer);
        if (hit.collider != null)
        {
            targetEnemy = hit.collider.GetComponent<Enemy>();
            Debug.Log("Enemy detected: " + hit.collider.name);
        }
    }

    private bool IsEnemyInRange(Enemy enemy)
    {
        return enemy != null && Vector2.Distance(transform.position, enemy.transform.position) <= attackRange;
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
        Debug.Log("Tower takes damage: " + damage);
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Tower has died.");
        Destroy(gameObject);
    }
}
