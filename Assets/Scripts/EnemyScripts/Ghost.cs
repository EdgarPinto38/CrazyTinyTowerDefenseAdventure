using UnityEngine;

public class Ghost : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        health = 100f; // Menos vida
        damage = 5f; // Menos da�o
        speed = 2f; // M�s r�pido
    }

    protected override void Move()
    {
        base.Move();
        Debug.Log("Ghost is moving."); 
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Ghost has been destroyed!"); // Eliminar si no es necesario
    }
}
