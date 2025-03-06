using UnityEngine;

public class Farmer : Tower
{
    protected override void Awake()
    {
        // Llamamos al Awake base para inicializar el SpriteRenderer y originalColor
        base.Awake();
    }

    private void Start()
    {
        health = 40f; 
        attackPower = 10f;
        attackRate = 2f; 
        attackRange = 5f;
      
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