using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 50f;
    public float damage = 10f;
    public float speed = 2f;
    public float attackRate = 1f; // Ataques por segundo
    public int pointsValue = 10;  // Puntos al morir

    [Header("Detection Settings")]
    public float detectionDistance = 5f; // Distancia de detección con raycast
    public LayerMask towerLayer; // Para filtrar solo las torres

    [Header("References")]
    public WaveController waveController;

    private EconomyManager economyManager;
    private Tower targetTower;
    private float attackCooldown = 0f;
    private float baseSpeed;

    protected virtual void InitializeStats()
    {
        // Valores predeterminados para enemigos básicos
        health = 50f;
        damage = 10f;
        speed = 2f;
        attackRate = 1f;
        pointsValue = 10;
    }

    protected virtual void Awake()
    {
        economyManager = FindObjectOfType<EconomyManager>();

        waveController = FindObjectOfType<WaveController>();

        if (waveController == null)
        {
            Debug.LogError("WaveController not found! Please make sure it exists in the scene.");
        }

        InitializeStats(); // Llamamos a este método para que los enemigos inicialicen sus valores
    }

    private void Start()
    {
        economyManager = FindObjectOfType<EconomyManager>();

      
        if (speed == 0f) speed = 2f; 
        baseSpeed = speed;

        if (waveController == null)
            Debug.LogError("WaveController not found! Please make sure it exists in the scene.");
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        // Detectamos torres primero
        DetectTower();

        // Si hay una torre objetivo, atacamos (si el cooldown lo permite)
        if (targetTower != null)
        {
            if (attackCooldown <= 0f)
                AttackTower();
        }
        else
        {
            // Si no hay torre, nos movemos
            Move();
        }

        if (IsOutOfBounds())
            HandleOutOfBounds();
    }

    protected virtual void Move() => transform.Translate(Vector3.left * speed * Time.deltaTime);

    private void DetectTower()
    {
        // Dibuja el rayo en el editor para debugging
        Debug.DrawRay(transform.position, Vector2.left * detectionDistance, Color.red);

        // Realiza el raycast hacia la izquierda
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            detectionDistance,
            towerLayer  
        );

        if (hit.collider != null)
        {
            // Verificar si el objeto tiene un componente Tower
            Tower towerComponent = hit.collider.GetComponent<Tower>();
            if (towerComponent != null)
            {
                // Si detectamos una torre, la asignamos como objetivo y nos detenemos
                targetTower = towerComponent;
                StopMoving();
                Debug.Log($"Tower detected at distance {hit.distance}. Stopping to attack.");
            }
            else
            {
                // Si el objeto no es una torre, limpiamos el objetivo y seguimos moviéndonos
                targetTower = null;
                ResumeMoving();
            }
        }
        else
        {
            // Si no detectamos nada, limpiamos el objetivo y seguimos moviéndonos
            targetTower = null;
            ResumeMoving();
        }
    }

    private void StopMoving()
    {
        speed = 0f;
    }

    private void ResumeMoving()
    {
        speed = baseSpeed;
    }

    private void AttackTower()
    {
        if (targetTower != null)
        {
            targetTower.TakeDamage(damage);
            attackCooldown = 1f / attackRate;
        }
    }

    private bool IsOutOfBounds() => transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.zero).x;

    private void HandleOutOfBounds()
    {
        waveController.EndLevel(false);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f) Die();
    }

    protected virtual void Die()
    {
        economyManager.AddPoints(pointsValue); // Añadir puntos según el valor del enemigo
        Destroy(gameObject);
    }
}