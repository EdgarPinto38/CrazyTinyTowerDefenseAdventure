using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public float health = 100f;
    public float attackPower = 10f;
    public float attackRange = 5f;
    public float attackRate = 1f; // Disparos por segundo

    [Header("Detection Settings")]
    public LayerMask enemyLayer; // Capa de los enemigos
    public float detectionInterval = 0.1f; // Frecuencia de detección

    [Header("Projectile System")]
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint; // Punto desde el cual se dispara el proyectil
    public ObjectPool projectilePool; // Referencia al Object Pool

    [Header("Visual Effects")]
    public float flashDuration = 0.5f;

    // Variables privadas - Eliminamos la declaración duplicada de attackCooldown
    private float _attackCooldown = 0f;  // Cambiamos el nombre para evitar cualquier confusión
    private float _detectionCooldown = 0f;  // También cambiamos esta para ser consistentes
    private Enemy targetEnemy;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    protected virtual void Awake()
    {
        // Obtener el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on this Tower!");
        }
    }

    private void Update()
    {
        // Actualizar contadores
        UpdateTimers();

        // Detectar enemigos cuando toca
        if (ShouldDetectEnemies())
            DetectEnemy();

        // Atacar cuando sea posible
        if (CanAttack())
            Attack();
        else if (targetEnemy == null || !IsEnemyInRange(targetEnemy))
            targetEnemy = null;
    }

    private void UpdateTimers()
    {
        _attackCooldown -= Time.deltaTime;
        _detectionCooldown -= Time.deltaTime;
    }

    private bool ShouldDetectEnemies()
    {
        if (_detectionCooldown <= 0f)
        {
            _detectionCooldown = detectionInterval;
            return true;
        }
        return false;
    }

    private bool CanAttack()
    {
        return _attackCooldown <= 0f && targetEnemy != null && IsEnemyInRange(targetEnemy);
    }

    private void DetectEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            shootPoint.position,
            Vector2.right,
            attackRange,
            enemyLayer
        );

        if (hit.collider != null)
        {
            targetEnemy = hit.collider.GetComponent<Enemy>();
            Debug.Log($"Enemy detected: {hit.collider.name}");
        }
    }

    private bool IsEnemyInRange(Enemy enemy)
    {
        if (enemy == null) return false;

        float distance = Vector2.Distance(transform.position, enemy.transform.position);
        return distance <= attackRange;
    }

    protected virtual void Attack()
    {
        if (!ValidateAttackComponents()) return;

        // Obtener y configurar el proyectil
        GameObject projectile = projectilePool.GetObject();
        if (projectile != null)
        {
            ConfigureProjectile(projectile);
            _attackCooldown = 1f / attackRate;
        }
    }

    private bool ValidateAttackComponents()
    {
        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogError("projectilePrefab or shootPoint is not assigned.");
            return false;
        }
        return true;
    }

    private void ConfigureProjectile(GameObject projectile)
    {
        projectile.transform.position = shootPoint.position;

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(attackPower);
            projScript.SetObjectPool(projectilePool);
            Debug.Log($"Tower attacks with power: {attackPower}");
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"Tower takes damage: {damage}");
        health -= damage;

        // Aplicar efecto visual de daño
        FlashRed();

        if (health <= 0f)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log("Tower has died.");

        // Detener cualquier corrutina de destello en ejecución
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }

        Destroy(gameObject);
    }

    private void FlashRed()
    {
        if (spriteRenderer == null) return;

        // Si hay una corrutina de destello en ejecución, la detenemos
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        // Limpiar la referencia
        flashCoroutine = null;
    }
}