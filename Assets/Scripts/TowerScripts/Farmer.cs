using UnityEngine;

public class Farmer : Tower
{
    private void Start()
    {
        health = 10f; // Poca vida
        attackPower = 5f;
        attackRate = 2f; // Disparo rápido
        attackRange = 4f;
        // El prefab del proyectil se asignará desde el Inspector, por lo que no necesitamos asignarlo aquí.
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Farmer attack!");
    }

    protected override void Die()
    {
        // Implementación específica de la muerte para Farmer
        base.Die();
        Debug.Log("Farmer has been destroyed!");
        // Añadir el código específico de muerte aquí
    }
}
