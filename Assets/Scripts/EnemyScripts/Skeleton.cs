using UnityEngine;

public class Skeleton : Enemy
{
    private void Start()
    {
        health = 50f; // Vida media
        damage = 12f; // Daño medio
        speed = 2.5f; // Velocidad media
    }

    protected override void Move()
    {
        base.Move();
        Debug.Log("Skeleton is moving.");
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Skeleton attacks!");
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Skeleton has been destroyed!");
    }
}
