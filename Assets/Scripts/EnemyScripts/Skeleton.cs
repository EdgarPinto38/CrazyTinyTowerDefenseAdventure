using UnityEngine;
using System.Collections;
public class Skeleton : Enemy
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine; // Variable para guardar referencia a la corrutina

    protected override void Awake()
    {
        base.Awake();
        health = 60f; 
        damage = 10f;
        attackRate = 1f;
        speed = 1f; 
        pointsValue = 20;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Guardar el color original del sprite
    }

    protected override void Move()
    {
        base.Move();


    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        // Si hay una corrutina de destello en ejecución, la detenemos
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        // Iniciamos una nueva corrutina y guardamos su referencia
        flashCoroutine = StartCoroutine(FlashRed());

    }

    protected override void Die()
    {
        base.Die();


        // Si hay una corrutina activa, la detenemos
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }

        spriteRenderer.color = originalColor; // Asegurarse de que el sprite tenga su color original
        Destroy(gameObject, 0.5f); // Destruir después de 1 segundo (opcional)
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red; // Cambiar el color a rojo
        yield return new WaitForSeconds(0.5f); // Esperar medio segundo
        spriteRenderer.color = originalColor; // Volver al color original
        flashCoroutine = null; // Limpiar la referencia cuando termina
    }
}
