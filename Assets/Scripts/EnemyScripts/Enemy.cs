using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 50f;
    public float damage = 10f;
    public float speed = 2f;
    public float attackRate = 1f; // Ataques por segundo
    public int pointsValue = 10;  // Puntos al morir

    [Header("References")]
    public WaveController waveController;

    private EconomyManager economyManager;
    private MonoBehaviour targetTower;
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

        waveController = FindObjectOfType<WaveController>(); // Asigna el WaveController mediante código

        if (waveController == null)
        {
            Debug.LogError("WaveController not found! Please make sure it exists in the scene.");
        }

        InitializeStats(); // Llamamos a este método para que los enemigos inicialicen sus valores
    }

    private void Start()
    {
        economyManager = FindObjectOfType<EconomyManager>();

        // Asegúrate de que speed tenga un valor antes de asignar baseSpeed
        if (speed == 0f) speed = 2f; // Puedes cambiar este valor según tus necesidades
        baseSpeed = speed;

        if (waveController == null)
            Debug.LogError("WaveController not found! Please make sure it exists in the scene.");
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (targetTower == null)
            Move();
        else if (attackCooldown <= 0f)
            AttackTower();

        DetectTower();

        if (IsOutOfBounds())
            HandleOutOfBounds();
    }

    protected virtual void Move() => transform.Translate(Vector3.left * speed * Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Tower tower))
        {
            AssignTargetTower(tower);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Tower tower) && targetTower == tower)
        {
            targetTower = null;
            ResumeMoving();
        }
    }

    private void AssignTargetTower(MonoBehaviour tower)
    {
        targetTower = tower;
        StopMoving();
    }

    private void StopMoving()
    {
        speed = 0f;
        Debug.Log("StopMoving called: speed set to 0");
    }

    private void ResumeMoving()
    {
        speed = baseSpeed;
        Debug.Log($"ResumeMoving called: speed = {speed}, baseSpeed = {baseSpeed}");
    }

    private void AttackTower()
    {
        if (targetTower.TryGetComponent(out Tower tower))
        {
            tower.TakeDamage(damage);
            attackCooldown = 1f / attackRate;
        }
    }

    private void DetectTower()
    {
        float rayDistance = 1f; // Distancia del raycast
        int layerMask = LayerMask.GetMask("Towers"); // Asegúrate de que las torres estén en la capa "Towers"

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit detected!");

            // Verificar si el objeto tiene un componente Tower o una clase derivada
            Tower towerComponent = hit.collider.GetComponent<Tower>();
            if (towerComponent != null)
            {
                targetTower = towerComponent;
                StopMoving();
                Debug.Log("Tower detected: Stopping the enemy.");
            }
            else
            {
                Debug.Log("Raycast hit, but no Tower component found.");
                targetTower = null;
                ResumeMoving();
            }
        }
        else
        {
            Debug.Log("No Raycast hit detected.");
            targetTower = null;
            ResumeMoving();
        }
    }




    private bool IsOutOfBounds() => transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.zero).x;

    private void HandleOutOfBounds()
    {
        waveController.EndLevel(false);
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f) Die();
    }

    protected virtual void Die()
    {
        economyManager.AddPoints(pointsValue);
        Destroy(gameObject);
    }
}
