using UnityEngine;

public enum WaveDifficulty
{
    Easy,
    Medium,
    Hard
}

public class WaveController : MonoBehaviour
{
    public Transform[] spawnPoints; // Puntos de spawn
    public GameObject[] easyEnemies; // Prefabs de enemigos fáciles
    public GameObject[] mediumEnemies; // Prefabs de enemigos medios
    public GameObject[] hardEnemies; // Prefabs de enemigos difíciles
    public float spawnInterval = 2f; // Intervalo entre spawns
    public float initialDelay = 15f; // Retardo inicial antes de que comience la oleada
    public WaveDifficulty difficulty = WaveDifficulty.Easy; // Dificultad de la oleada

    private float spawnTimer = 0f;
    private float delayTimer;

    private void Start()
    {
        delayTimer = initialDelay;
    }

    private void Update()
    {
        // Esperar el tiempo de retardo inicial antes de empezar la oleada
        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        // Seleccionar un punto de spawn aleatorio
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Seleccionar un enemigo aleatorio basado en la dificultad
        GameObject enemyPrefab = SelectEnemyBasedOnDifficulty();

        // Instanciar el enemigo
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    private GameObject SelectEnemyBasedOnDifficulty()
    {
        GameObject[] selectedEnemies;
        switch (difficulty)
        {
            case WaveDifficulty.Easy:
                selectedEnemies = easyEnemies;
                break;
            case WaveDifficulty.Medium:
                selectedEnemies = mediumEnemies;
                break;
            case WaveDifficulty.Hard:
                selectedEnemies = hardEnemies;
                break;
            default:
                selectedEnemies = easyEnemies;
                break;
        }

        // Seleccionar un enemigo aleatorio del array seleccionado
        return selectedEnemies[Random.Range(0, selectedEnemies.Length)];
    }
}
