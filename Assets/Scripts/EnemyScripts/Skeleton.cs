using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        health = 1f; // Menos vida
        damage = 8f; // Menos daño
        speed = 2f; // Más rápido
        pointsValue = 50;
    }

    protected override void Move()
    {
        base.Move();
        Debug.Log("Skeleton is moving.");
    }

   

    protected override void Die()
    {
        base.Die();
        Debug.Log("Skeleton has been destroyed!");
    }
}
