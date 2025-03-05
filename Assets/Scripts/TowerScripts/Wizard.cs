using UnityEngine;

public class Wizard : Tower
{
    private void Start()
    {
        health = 20f; // Vida media
        attackPower = 40f; // Mucho da�o
        attackRate = 0.5f; // Disparo lento
        attackRange = 7f;
    }

    protected override void Attack()
    {
        // Implementaci�n espec�fica del ataque para Tower2
        base.Attack();
        Debug.Log("Tower2 special attack!");
        // A�adir el c�digo espec�fico de ataque aqu�
    }

    protected override void Die()
    {
        // Implementaci�n espec�fica de la muerte para Tower2
        base.Die();
        Debug.Log("Tower2 has been destroyed!");
        // A�adir el c�digo espec�fico de muerte aqu�
    }
}
