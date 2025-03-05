using UnityEngine;

public class Farmer : Tower
{
    private void Start()
    {
        health = 50f; // Poca vida
        attackPower = 50f;
        attackRate = 2f; // Disparo rápido
        attackRange = 5f;
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
