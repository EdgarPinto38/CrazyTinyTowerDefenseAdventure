using UnityEngine;

public class Demon : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        health = 100f;
        damage = 20f;
        speed = 1.5f;
    }

    protected override void Move()
    {
        base.Move();
        // Debug.Log("Demon is moving."); // Eliminar si no es necesario
    }

    protected override void Die()
    {
        base.Die();
        // Debug.Log("Demon has been destroyed!"); // Eliminar si no es necesario
    }
}
