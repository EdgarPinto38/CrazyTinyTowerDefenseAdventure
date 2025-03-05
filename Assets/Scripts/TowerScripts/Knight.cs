using UnityEngine;

public class Knight : Tower
{
    private void Start()
    {
        health = 200f; // Mucha vida
        attackPower = 50f; // Mucho da�o
        attackRate = 0.2f; // Extremadamente lento
        attackRange = 2f; // Ataque cercano
    }

    protected override void Attack()
    {
        // Implementaci�n espec�fica del ataque para Tower3
        base.Attack();
        Debug.Log("Tower3 special attack!");
        // A�adir el c�digo espec�fico de ataque aqu�
    }

    protected override void Die()
    {
        // Implementaci�n espec�fica de la muerte para Tower3
        base.Die();
        Debug.Log("Tower3 has been destroyed!");
        // A�adir el c�digo espec�fico de muerte aqu�
    }
}
