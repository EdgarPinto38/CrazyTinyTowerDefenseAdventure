using UnityEngine;

public class Farmer : Tower
{
    private void Start()
    {
        health = 50f; // Poca vida
        attackPower = 50f;
        attackRate = 2f; // Disparo r�pido
        attackRange = 5f;
        // El prefab del proyectil se asignar� desde el Inspector, por lo que no necesitamos asignarlo aqu�.
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Farmer attack!");
    }

    protected override void Die()
    {
        // Implementaci�n espec�fica de la muerte para Farmer
        base.Die();
        Debug.Log("Farmer has been destroyed!");
        // A�adir el c�digo espec�fico de muerte aqu�
    }
}
