using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float damage = 10f;
    public float attackRate = 1f; // Ataques por segundo
    public float speed = 2f;
    public float attackRange = 2f; // Rango de ataque

    private float attackCooldown = 0f;
    private bool isAttacking = false;

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (!isAttacking)
        {
            Move();
        }

        if (attackCooldown <= 0f && IsTowerInRange())
        {
            Attack();
            attackCooldown = 1f / attackRate;
        }
    }

    protected virtual void Move()
    {
        // Movimiento básico hacia la izquierda
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Verificar si hay una torre en el rango de ataque
        if (IsTowerInRange())
        {
            isAttacking = true; // Detener el movimiento y comenzar a atacar
        }
    }

    protected virtual void Attack()
    {
        // Lógica para atacar una torre
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange);
        if (hit.collider != null)
        {
            Tower tower = hit.collider.GetComponent<Tower>();
            if (tower != null)
            {
                tower.TakeDamage(damage);
                Debug.Log("Enemy attacks tower for " + damage + " damage.");
            }
        }

        // Continuar atacando mientras haya una torre en rango
        if (!IsTowerInRange())
        {
            isAttacking = false; // Reanudar el movimiento si no hay torre en rango
        }
    }

    protected bool IsTowerInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange);
        return hit.collider != null && hit.collider.GetComponent<Tower>() != null;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Implementación general de la muerte del enemigo
        Debug.Log("Enemy has died.");
        Destroy(gameObject);
    }
}
