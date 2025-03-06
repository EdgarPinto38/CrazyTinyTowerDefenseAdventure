using UnityEngine;

public class Knight : Tower
{
    protected override void Awake()
    {
        // Llamamos al Awake base para inicializar el SpriteRenderer y originalColor
        base.Awake();
    }

    private void Start()
    {
        health = 80f;
        attackPower = 20f;
        attackRate = 1f;
        attackRange = 3f;

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
