using UnityEngine;

public class Knight : Tower
{
    private void Start()
    {
        health = 200f; // Mucha vida
        attackPower = 50f; // Mucho daño
        attackRate = 0.2f; // Extremadamente lento
        attackRange = 2f; // Ataque cercano
    }

    protected override void Attack()
    {
        // Implementación específica del ataque para Tower3
        base.Attack();
        Debug.Log("Tower3 special attack!");
        // Añadir el código específico de ataque aquí
    }

    protected override void Die()
    {
        // Implementación específica de la muerte para Tower3
        base.Die();
        Debug.Log("Tower3 has been destroyed!");
        // Añadir el código específico de muerte aquí
    }
}
