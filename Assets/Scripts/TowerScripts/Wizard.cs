using UnityEngine;

public class Wizard : Tower
{
    private void Start()
    {
        health = 20f; // Vida media
        attackPower = 40f; // Mucho daño
        attackRate = 0.5f; // Disparo lento
        attackRange = 7f;
    }

    protected override void Attack()
    {
        // Implementación específica del ataque para Tower2
        base.Attack();
        Debug.Log("Tower2 special attack!");
        // Añadir el código específico de ataque aquí
    }

    protected override void Die()
    {
        // Implementación específica de la muerte para Tower2
        base.Die();
        Debug.Log("Tower2 has been destroyed!");
        // Añadir el código específico de muerte aquí
    }
}
