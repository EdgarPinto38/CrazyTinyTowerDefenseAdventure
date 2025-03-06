using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public TMP_Text enemiesLeftText; // Texto de TextMeshPro para mostrar los enemigos restantes

    public int easyEnemiesToKill = 10; // Cantidad de enemigos a eliminar para dificultad fácil
    public int mediumEnemiesToKill = 20; // Cantidad de enemigos a eliminar para dificultad media
    public int hardEnemiesToKill = 30; // Cantidad de enemigos a eliminar para dificultad difícil

    public GameObject winPanel; // Panel de ganar
    public GameObject losePanel;

    private int totalEnemies;
    private int enemiesKilled;
    private float spawnTimer = 0f;
    private float delayTimer;
    private bool levelEnded = false;

    private void Start()
    {
        difficulty = GameSettings.SelectedDifficulty; // Asignar la dificultad seleccionada

        delayTimer = initialDelay;

        // Establecer el número total de enemigos según la dificultad
        switch (difficulty)
        {
            case WaveDifficulty.Easy:
                totalEnemies = easyEnemiesToKill;
                break;
            case WaveDifficulty.Medium:
                totalEnemies = mediumEnemiesToKill;
                break;
            case WaveDifficulty.Hard:
                totalEnemies = hardEnemiesToKill;
                break;
        }

        UpdateEnemiesLeftUI();
    }

    private void Update()
    {
        if (levelEnded) return;

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

        if (totalEnemies <= 0 && AreAllEnemiesDefeated())
        {
            EndLevel(true);
        }
    }

    private void SpawnEnemy()
    {
        if (totalEnemies <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];
        GameObject enemyPrefab = SelectEnemyBasedOnDifficulty();
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Asignar WaveController al enemigo instanciado
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.waveController = this;
        }

        totalEnemies--;
        UpdateEnemiesLeftUI();
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

        return selectedEnemies[Random.Range(0, selectedEnemies.Length)];
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateEnemiesLeftUI();

        if (enemiesKilled >= totalEnemies && AreAllEnemiesDefeated())
        {
            EndLevel(true);
        }
    }

    private void UpdateEnemiesLeftUI()
    {
        if (enemiesLeftText != null)
        {
            int enemiesLeft = totalEnemies;
            enemiesLeftText.text = "Enemies Left: " + enemiesLeft;
        }
    }

    public void EnemyPassed()
    {
        EndLevel(false);
    }

    public void EndLevel(bool isWin)
    {
        levelEnded = true;
        Debug.Log(isWin ? "Level Completed!" : "Level Failed!");

        if (isWin)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }

        // Destruir todos los enemigos restantes en la escena
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        // Moverse a la escena LevelSelector después de 3 segundos
        StartCoroutine(MoveToMainMenuAfterDelay(3f));
    }

    private IEnumerator MoveToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }

    private bool AreAllEnemiesDefeated()
    {
        return FindObjectsOfType<Enemy>().Length == 0;
    }
}
