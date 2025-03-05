using UnityEngine;

public class Ghost : Enemy
{
    private void Start()
    {
        health = 30f; // Menos vida
        damage = 8f; // Menos daño
        speed = 3f; // Más rápido
    }

    protected override void Move()
    {
        base.Move();
        Debug.Log("Ghost is moving.");
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Ghost attacks!");
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Ghost has been destroyed!");
    }
}
