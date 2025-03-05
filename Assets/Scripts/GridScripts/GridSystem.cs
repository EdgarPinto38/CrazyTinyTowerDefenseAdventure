using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int rows = 5;
    public int columns = 8;
    public float cellWidth = 2f;
    public float cellHeight = 1f;
    public float tolerance = 1f; // Ajusta esto para aumentar el área de soltar
    public GameObject pointPrefab;
    public Vector2 gridPosition = new Vector2(0, 0);
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        Vector3 gridCenter = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        transform.position = new Vector3(gridCenter.x - (columns * cellWidth / 2) + gridPosition.x, gridCenter.y - (rows * cellHeight / 2) + gridPosition.y, 0);

        DrawGridPoints();
    }

    void DrawGridPoints()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 cellPosition = new Vector3(
                    transform.position.x + (j * cellWidth),
                    transform.position.y + (i * cellHeight),
                    0);

                Instantiate(pointPrefab, cellPosition, Quaternion.identity);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (mainCamera == null) return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 cellPosition = new Vector3(
                    transform.position.x + (j * cellWidth),
                    transform.position.y + (i * cellHeight),
                    0);

                // Dibuja un círculo para visualizar la tolerancia
                Gizmos.DrawWireSphere(cellPosition, tolerance * Mathf.Min(cellWidth, cellHeight) / 2);
            }
        }
    }
}
