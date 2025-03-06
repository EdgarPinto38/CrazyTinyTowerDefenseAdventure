using UnityEngine;

public class Wizard : Tower
{
    protected override void Awake()
    {
        // Llamamos al Awake base para inicializar el SpriteRenderer y originalColor
        base.Awake();
    }

    private void Start()
    {
        health = 20f;
        attackPower = 40f;
        attackRate = 0.5f;
        attackRange = 8f;

    }

    protected override void Attack()
    {
        base.Attack();

    }

    protected override void Die()
    {

        base.Die();
        Debug.Log("Farmer has been destroyed!");
    }
}
