using UnityEngine;

public class Demon : Enemy
{
    private void Start()
    {
        health = 100f; // Mucha vida
        damage = 20f; // Mucho da�o
        speed = 1.5f; // M�s lento
    }

    protected override void Move()
    {
        base.Move();
        Debug.Log("Demon is moving.");
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Demon attacks!");
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Demon has been destroyed!");
    }
}
